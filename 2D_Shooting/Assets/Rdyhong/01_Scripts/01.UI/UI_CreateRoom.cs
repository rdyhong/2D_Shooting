using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_CreateRoom : UIWindow
{
    [SerializeField] TMP_InputField roomName;
    [SerializeField] TMP_InputField maxPlayer;

    public override void Init()
    {
        OnButtonBinding();

        // Maxplayer InputField End Edit Callback Set
        maxPlayer.onEndEdit.AddListener((_str) => {
            int _val = 4;
            if (!int.TryParse(_str, out _val))
            {
                maxPlayer.text = "4";
            }

            if (_val > 6 || _val < 0 || _val % 2 != 0)
                maxPlayer.text = "4";
        });
    }

    public override void OnButtonBinding()
    {
        foreach (Button _btn in buttons)
        {
            switch (_btn.name)
            {
                case "MainBtn_CreateRoom":
                    _btn.onClick.AddListener(() => {
                        Open(false);
                    });
                    break;
                case "BG_CreateRoomClose":
                    _btn.onClick.AddListener(() => {
                        Close(false);
                    });
                    break;
                case "Btn_CreateRoom":
                    _btn.onClick.AddListener(() => {
                        CreateRoomBtnAction();
                    });
                    break;
            }
        }
    }

    public override void Open(bool force = false, float duration = 0.5F, Action callback = null, Action fallback = null)
    {
        if (Photon_Room.isInRoom) return;

        base.Open(force, duration, callback, fallback);

    }
    public override void Close(bool force = false, float duration = 0.5F, Action callback = null)
    {
        base.Close(force, duration, callback);

    }

    public override void TextSetting()
    {
    }

    public override void DeInit()
    {
    }

    void CreateRoomBtnAction()
    {
        if (roomName.text == string.Empty)
        {
            NoticeMgr.AddNotice("Set Room Name", "Room Setting");
            return;
        }
        if (maxPlayer.text == string.Empty)
        {
            NoticeMgr.AddNotice("Set Player", "Player Setting");
            return;
        }

        Close();

        int _maxPlayer = int.Parse(maxPlayer.text);

        Photon_Room.CreatedRoom(roomName.text, Photon_Room.SetRoomOption(_maxPlayer, true, true));
    }
    
}
