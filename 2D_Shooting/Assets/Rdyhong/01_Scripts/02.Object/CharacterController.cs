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

    Gun equipedGun = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        aimPos = transform.Find("AimPos");
        camPos = transform.Find("CamPos");
    }

    private void Start()
    {
        // Test
        Init();
    }

    public void Init()
    {
        CameraController.SetCameraTarget(camPos);
        InGameMgr.SetMyCharacter(this);

        sprintSpeed = baseSpeed + baseSpeed * 0.3f;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Move();
            Aimming();
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

    
}
