using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        InputManager.isFire = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputManager.isFire = false;
    }
}
