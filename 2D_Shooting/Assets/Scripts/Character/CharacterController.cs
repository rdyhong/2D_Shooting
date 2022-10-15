using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] Transform aim;

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
    }

    
}
