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
    }

    void Move()
    {
        transform.position = transform.position + InputManager.Instance.dir * speed * Time.deltaTime;
    }

    void Rot()
    {
        transform.up = InputManager.Instance.dir;//aim.position - transform.position;
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

        }
        else
        {
            if (!isOnAim) return;


        }
    }
    
}
