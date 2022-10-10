using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private float x = 0;
    private float y = 0;

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
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        
    }
}
