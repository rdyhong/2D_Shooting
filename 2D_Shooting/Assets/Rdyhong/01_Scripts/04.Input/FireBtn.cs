using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        InputMgr.isFire = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputMgr.isFire = false;
    }
}
