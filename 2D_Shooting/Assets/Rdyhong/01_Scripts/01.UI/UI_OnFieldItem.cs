using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_OnFieldItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nameTmp;
    [SerializeField] Button btn;

    Transform targetTf;
    Action BtnCB = null;

    bool isActive = false;

    private void Start()
    {
        

        
    }

    public void Init(Transform _targetTf, string _name, Action _action)
    {
        gameObject.SetActive(false);

        transform.SetParent(InGameMgr.Inst.ItemUIParent);

        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(Util.GetScreenSize().x / 7, Util.GetScreenSize().y / 12);

        btn.onClick.AddListener(() => {
            BtnCB?.Invoke();
        });

        targetTf = _targetTf;
        _nameTmp.text = _name;
        BtnCB = _action;
    }

    private void Update()
    {
        if (!isActive) return;

        transform.position = Camera.main.WorldToScreenPoint(targetTf.position + Vector3.up);
    }

    public void Active(bool _active)
    {
        if (isActive == _active) return;

        isActive = _active;

        if(isActive)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void Recycle()
    {
        targetTf = null;
        BtnCB = null;
        ObjectPool.Recycle(this);
    }
}
