using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public Vector2 dir;
    //private float x = 0; // Move direction X
    //private float y = 0; // Move direction Y

    public static bool isAimming = false; // 조준시 True

    public Vector3 Dir
    {
        get
        {
            Vector2 d = dir;
            d = Vector2.ClampMagnitude(d, 1); // 길이를 1로 제한
            return d;
        }
    }

    
    void Update()
    {
        //PCInput();

    }

    void PCInput()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.y = Input.GetAxis("Vertical");

        // Aim Check
#if Unity_Editer
        if (Input.GetMouseButtonDown(1))
        {
            isAimming = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isAimming = false;
        }
#else
        // Button

#endif

    }
}
