using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RoomList : UIWindow
{
    [SerializeField] Transform contentParent;
    private List<Content_RoomListEle> roomElements = new List<Content_RoomListEle>();


    public override void Init()
    {

        OnButtonBinding();
    }

    public override void OnButtonBinding()
    {
        foreach(Button _btn in buttons)
        {
            switch (_btn.name)
            {
                case "MainBtn_RoomList":
                    _btn.onClick.AddListener(() => {
                        Open(false);
                    });
                    break;
                case "BG_RoomsListClose":
                    _btn.onClick.AddListener(() => {
                        Close(false);
                    });
                    break;
            }
        }
    }

    public override void TextSetting()
    {
    }

    public override void Open(bool force = false, float duration = 0.5F, Action callback = null, Action fallback = null)
    {
        base.Open(force, duration, callback, fallback);
        RoomListRefresh();

    }
    public override void Close(bool force = false, float duration = 0.5F, Action callback = null)
    {
        base.Close(force, duration, callback);

    }
    public override void DeInit()
    {
        RemoveAllRoomList();
    }

    void RoomListRefresh()
    {
        RemoveAllRoomList();

        List<Photon.Realtime.RoomInfo> allRoom = Photon_Room.GetAllRoomInfo();

        foreach(Photon.Realtime.RoomInfo _info in Photon_Room.GetAllRoomInfo())
        {
            if (!_info.IsOpen || !_info.IsVisible) continue;

            Content_RoomListEle _ele = ObjectPool.Spawn<Content_RoomListEle>("Content_RoomListEle");
            _ele.transform.SetParent(contentParent);
            _ele.transform.localScale = Vector3.one;
            _ele.Init(_info);
            roomElements.Add(_ele);
        }
    }

    void RemoveAllRoomList()
    {
        for (int i = 0; i < roomElements.Count; i++)
        {
            roomElements[i].Recycle();
        }

        roomElements.Clear();
    }
}
