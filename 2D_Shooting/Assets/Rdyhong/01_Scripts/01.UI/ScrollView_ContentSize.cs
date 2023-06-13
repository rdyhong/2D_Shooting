using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView_ContentSize : MonoBehaviour
{
    RectTransform rt;
    RectTransform parentRt;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        parentRt = transform.parent.parent.GetComponent<RectTransform>();

        //rt.sizeDelta = new Vector2(parentRt.rect.width, rt.sizeDelta.y);
        //Util.WidthMatchToParent(rt, parentRt);
    }
}
