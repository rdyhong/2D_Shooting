using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_JoinRoom : UIWindow
{
    [SerializeField] TMP_InputField inputFieldTmp;
    public override void Init()
    {
        OnButtonBinding();
    }

    public override void OnButtonBinding()
    {
        foreach (Button _btn in buttons)
        {
            switch (_btn.name)
            {
                case "MainBtn_JoinRoom":
                    _btn.onClick.AddListener(() => {
                        Open();
                    });
                    break;
                case "BG_JoinRoomClose":
                    _btn.onClick.AddListener(() => {
                        Close();
                    });
                    break;
                case "Btn_JoinRoom":
                    _btn.onClick.AddListener(() => {
                        Photon_Room.JoinRoom($"{inputFieldTmp.text}");
                    });
                    break;
            }
        }
    }

    public override void Open(bool force = false, float duration = 0.5F, Action callback = null, Action fallback = null)
    {
        base.Open(force, duration, callback, fallback);

    }
    public override void Close(bool force = false, float duration = 0.5F, Action callback = null)
    {
        base.Close(force, duration, callback);


    }

    public override void DeInit()
    {
        
    }

    public override void TextSetting()
    {
    }
}
