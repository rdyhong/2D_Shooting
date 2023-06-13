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

    private void Start()
    {
        btn.onClick.AddListener(() => {
            BtnCB?.Invoke();
        });
    }

    public void Spawn(Transform _targetTf, string _name, Action _action)
    {
        targetTf = _targetTf;
        _nameTmp.text = _name;
        BtnCB = _action;
    }

    private void Update()
    {
        if (targetTf == null) return;

        transform.position = targetTf.position;
    }

    public void Recycle()
    {
        ObjectPool.Recycle(this);
    }
}
