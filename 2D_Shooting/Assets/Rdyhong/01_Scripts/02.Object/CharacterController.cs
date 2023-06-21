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

    Gun equiped_Main = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        aimPos = transform.Find("AimPos");
        camPos = transform.Find("CamPos");
        gunPos = transform.Find("GunPos");
    }

    private void Start()
    {
        // Test
        if(photonView.IsMine)
            Init();
    }

    public void Init()
    {
        CameraController.SetCameraTarget(camPos);
        InGameMgr.SetMyCharacter(this);

        sprintSpeed = baseSpeed + baseSpeed * 0.3f;

        photonView.RPC("RPC_Init", RpcTarget.All);
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
        photonView.RPC("RPC_EquipItem", RpcTarget.All, idx);
        
    }

    public void DropItem()
    {
        //photonView.RPC("RPC_DropItem", RpcTarget.All, null);

        
    }

    [PunRPC]
    void RPC_EquipItem(Gun _equip)
    {
        DropItem();

        equiped_Main = _equip;
        //_equip.Equip(gunPos, photonView.IsMine);
    }

    [PunRPC]
    void RPC_DropItem()
    {
        if (equiped_Main == null) return;

        equiped_Main.Drop(transform.up);
        equiped_Main = null;
    }
}
