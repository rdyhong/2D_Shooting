using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float speed;
    float rotSpeed = 0.2f;
    private Transform aimPos;
    private bool isOnAim = false;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        aimPos = transform.Find("AimPos");
    }

    void Update()
    {
        Move();
        Rot();
        Aimming();
    }

    void Move()
    {
        Vector3 nextPos;
        float _speed = speed;
        
        if (InputManager.isRun)
        {
            _speed *= 1.4f;
        }

        nextPos = transform.position + InputManager.Instance.Dir * (_speed * InputManager.weight) * TimeManager.ObjDeltaTime;
        transform.position = nextPos;
    }

    void Rot()
    {
        Vector3 target = Vector3.zero;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, InputManager.Instance.Rot, 0.2f);
    }

    void Aimming()
    {
        if(InputManager.isAimming && !isOnAim)
        {
            isOnAim = true;
            CameraController.SetCameraTarget(aimPos);
        }
        if(!InputManager.isAimming && isOnAim)
        {
            isOnAim = false;
        }
    }
    

}
