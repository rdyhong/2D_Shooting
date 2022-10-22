using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private float x = 0; // Move direction X
    private float y = 0; // Move direction Y

    public static bool isAimming = false; // 조준시 True

    public Vector3 dir
    {
        get
        {
            Vector3 d = new Vector3(x, y, 0);
            d = Vector3.ClampMagnitude(d, 1); // 길이를 1로 제한
            return d;
        }
    }

    
    void Update()
    {
        PCInput();

    }

    void PCInput()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

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
