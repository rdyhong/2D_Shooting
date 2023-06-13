using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Realtime;
using TMPro;

public class Content_RoomListEle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roomNameTmp;
    [SerializeField] TextMeshProUGUI playerCountTmp;

    string roomName = string.Empty;
    string playerCount = string.Empty;

    [SerializeField] Button joinBtn;


    public void Init(RoomInfo _info)
    {
        Util.WidthMatchToParent(transform.GetComponent<RectTransform>(), transform.parent.parent.GetComponent<RectTransform>());

        roomName = _info.Name;
        playerCount = $"{_info.PlayerCount} / {_info.MaxPlayers}";

        roomNameTmp.text = roomName;
        playerCountTmp.text = playerCount;

        joinBtn.onClick.AddListener(() =>
        {
            Photon_Room.JoinRoom(roomName);
        });
    }

    public void Recycle()
    {
        ObjectPool.Recycle(this);
    }    
}
