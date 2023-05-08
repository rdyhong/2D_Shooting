using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RunBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        InputManager.isRun = true;
        Debug.Log($"RunButton ::: {InputManager.isRun}");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputManager.isRun = false;
        Debug.Log($"RunButton ::: {InputManager.isRun}");
    }
}
