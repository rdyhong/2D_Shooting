using UnityEngine;

public class TimeManager
{
    private static float objTimeScale = 1;
    public static float ObjDeltaTime
    {
        get
        {
            return Time.deltaTime * objTimeScale;
        }
        set
        {
            if (value <= 0)
            {
                objTimeScale = 0f;
                Debug.LogError("Can't set time to lower than 0");
            }
            objTimeScale = value;
            Debug.Log($"UIDeltaTime Set ::: {objTimeScale}");
        }
    }

    private static float uiTimeScale = 1;
    public static float UIDeltaTime
    {
        get
        {
            return Time.deltaTime * uiTimeScale;
        }
        set
        {
            if (value <= 0)
            {
                uiTimeScale = 0f;
                Debug.LogError("Can't set time to lower than 0");
            }

            uiTimeScale = value;
            Debug.Log($"UIDeltaTime Set ::: {uiTimeScale}");
        }
    }

}
