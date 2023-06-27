using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class CharacterController : MonoBehaviourPun, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation.eulerAngles);
        }
        else
        {
            transform.DOMove((Vector3)stream.ReceiveNext(), 0.1f).SetEase(Ease.Linear);
            transform.DORotate((Vector3)stream.ReceiveNext(), 0.1f).SetEase(Ease.Linear);
        }
    }


    [SerializeField] private float speed;
    private float baseSpeed = 8;
    private float sprintSpeed;
    private bool isOnAim = false;

    private Rigidbody2D rb;

    private Transform aimPos;
    private Transform camPos;
    private Transform gunPos;
    private GameObject obj_nightVision;
    private GameObject obj_baseLight;

    Item_Base equiped_Item = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        aimPos = transform.Find("AimPos");
        camPos = transform.Find("CamPos");
        gunPos = transform.Find("GunPos");
        obj_nightVision = transform.Find("NightVision").gameObject;
        obj_baseLight = transform.Find("BaseLight").gameObject;
    }

    private void Start()
    {
        // Test
        
            Init();
    }

    public void Init()
    {
        if (photonView.IsMine)
        {
            CameraController.SetCameraTarget(camPos);

            InGameMgr.SetMyCharacter(this);

            sprintSpeed = baseSpeed + baseSpeed * 0.3f;

            photonView.RPC("RPC_Init", RpcTarget.All);

            obj_nightVision.SetActive(true);
            obj_baseLight.SetActive(true);

            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            obj_nightVision.SetActive(false);
            obj_baseLight.SetActive(false);

            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    [PunRPC]
    void RPC_Init()
    {
        transform.SetParent(InGameMgr.Inst.playerParent);

        string nickName = PhotonNetwork.LocalPlayer.NickName == string.Empty ? "NoName" : PhotonNetwork.LocalPlayer.NickName;
        gameObject.name = $"Player_{nickName}";
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Move();
            Aimming();

            // Test
            if(Input.GetKeyDown(KeyCode.D))
            {
                DropItem();
            }

            if (InputMgr.isFire)
            {
                if(equiped_Item != null)
                    equiped_Item.Use();
            }
        }
    }

    void Move()
    {
        if (InputMgr.isRun)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = baseSpeed;
        }

        Vector3 nextPos = transform.position + (Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward) * InputMgr.Inst.Dir) * speed * InputMgr.weight * TimeMgr.ObjDeltaTime;
        rb.MovePosition(nextPos);

        //rb.position = transform.position + InputMgr.Inst.Dir * speed * InputMgr.weight * TimeMgr.ObjDeltaTime;
        //transform.position = nextPos;
    }

    /*
    void Rot()
    {
        Vector3 target = Vector3.zero;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, InputManager.Inst.Rot, rotSpeed * TimeMgr.ObjDeltaTime);
    }
    */

    public void SetRotate(float _rotVal)
    {
        transform.Rotate(Vector3.forward, _rotVal);
    }

    void Aimming()
    {
        if(InputMgr.isAimming && !isOnAim)
        {
            isOnAim = true;
            //CameraController.SetCameraTarget(aimPos);
        }
        if(!InputMgr.isAimming && isOnAim)
        {
            isOnAim = false;
        }
    }

    public void EquipItem(int idx)
    {
        photonView.RPC(nameof(RPC_EquipItem_Proccess), RpcTarget.MasterClient, idx);
    }

    [PunRPC]
    void RPC_EquipItem_Proccess(int _idx)
    {
        // Check on MasterClient

        if(PhotonMgr.ingame.allWeapons[_idx].ownerExist)
        {
            DebugMgr.LogErr($"Index_[{_idx}] -> Already Owner Exist");
            return;
        }
        
        photonView.RPC(nameof(RPC_EquipItem), RpcTarget.All, _idx);
        //equiped_Main = _equip;
        //_equip.Equip(gunPos, photonView.IsMine);
    }

    [PunRPC]
    void RPC_EquipItem(int _idx)
    {
        if(equiped_Item != null) RPC_DropItem();

        equiped_Item = PhotonMgr.ingame.allWeapons[_idx];
        equiped_Item.Equip(this, gunPos);
    }


    public void DropItem()
    {
        photonView.RPC(nameof(RPC_DropItem_Proccess), RpcTarget.MasterClient, equiped_Item.idx);
    }

    [PunRPC]
    void RPC_DropItem_Proccess(int _idx)
    {
        // Check on MasterClient
        if (!PhotonMgr.ingame.allWeapons[_idx].ownerExist) return;

        photonView.RPC(nameof(RPC_DropItem), RpcTarget.All, null);
    }

    [PunRPC]
    void RPC_DropItem()
    {
        equiped_Item.Drop(transform.up);
        equiped_Item = null;
    }
}
