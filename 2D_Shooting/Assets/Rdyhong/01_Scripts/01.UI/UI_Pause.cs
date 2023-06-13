using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pause : UIWindow
{
    [SerializeField] List<GameObject> contents;

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
                case "Btn_Pause":
                    _btn.onClick.AddListener(() => {
                        Open();
                    });
                    break;
                case "BG_UI_Pause":
                    _btn.onClick.AddListener(() => {
                        Close();
                    });
                    break;
                case "Btn_GamePlay":
                    _btn.onClick.AddListener(() => {

                    });
                    break;
            }
        }
    }

    public override void DeInit()
    {
        
    }

    public override void Open(bool force = false, float duration = 0.5F, Action callback = null, Action fallback = null)
    {
        if (!Photon_Room.isInRoom) return;
        TextSetting();
        OpenWindow();

        base.Open(force, duration, callback, fallback);

    }
    public override void Close(bool force = false, float duration = 0.5F, Action callback = null)
    {
        base.Close(force, duration, callback);

    }

    void OpenWindow(string _name = "")
    {
        if(_name.Equals(""))
        {
            _name = contents[0].name;
        }

        for(int i = 0; i < contents.Count; i++)
        {
            if (contents[i].name == _name)
            {
                contents[i].SetActive(true);
                continue;
            }
            contents[i].SetActive(false);
        }
    }

    public override void TextSetting()
    {
        GetTmp("Ping_TMP").text = $"Ping ( {Photon_Controller.GetPing()} )";
    }
}
