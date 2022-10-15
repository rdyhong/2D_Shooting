using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    bool isAimming = false;

    Transform aimPos;
    Vector2 mousePos;
    Camera cam;

    private void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        
    }
    
    void Update()
    {
        

        //mousePos = Input.mousePosition;
        //Vector3 camPos = cam.ScreenToWorldPoint(mousePos);
        //camPos.z = 0;
        //transform.position = camPos; //Input.mousePosition;
        
    }

    void Aimming()
    { 

    }

}
