using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : Singleton<InputMgr>
{
    // Character Move direction
    private Vector2 dir = Vector2.zero;
    // Move weight
    public static float weight = 0;

    // aim button pressed
    public static bool isAimming = false; 
    // fire button pressed
    public static bool isFire = false;
    // Run Button Pressed
    public static bool isRun = false;

    // Character rotation
    public Quaternion Rot;
    public static float rotXValue = 0;

    // Use this direction
    public Vector3 Dir
    {
        get
        {
            Vector2 d = dir;
            //d = Vector2.ClampMagnitude(d, 1);
            return d;
        }
        set
        {
            //dir = Vector2.ClampMagnitude(value, 1);
            dir = value.normalized;
        }
    }

    
    void Update()
    {
        //PCInput();
    }

    void PCInput()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

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
