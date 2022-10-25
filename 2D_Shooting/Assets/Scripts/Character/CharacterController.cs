using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] Transform aim;
    private bool isOnAim = false;

    Rigidbody2D rb;


    private void Awake()
    {

    }

    void Update()
    {
        Move();
        Rot();
        Aimming();
    }

    void Move()
    {
        transform.position = transform.position + InputManager.Instance.Dir * speed * Time.deltaTime;
    }

    void Rot()
    {
        transform.up = InputManager.Instance.Dir;//aim.position - transform.position;
        if (transform.localEulerAngles.x != 0)
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    } 

    void Aimming()
    {
        if(InputManager.isAimming && !isOnAim)
        {
            isOnAim = true;
            CameraController.Instance.SetCameraTarget(aim);
        }
        if(!InputManager.isAimming && isOnAim)
        {
            isOnAim = false;


        }
    }
    
}
