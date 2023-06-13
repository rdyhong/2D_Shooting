using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RunBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        InputMgr.isRun = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputMgr.isRun = false;
    }
}
