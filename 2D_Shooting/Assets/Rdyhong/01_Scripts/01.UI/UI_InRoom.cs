using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InRoom : UIWindow
{
    [SerializeField] Transform contentParent_Red;
    [SerializeField] Transform contentParent_Blue;

    private List<Content_RoomPlayerInfo> playerElements = new List<Content_RoomPlayerInfo>();
    Button startBtn;

    public override void Init()
    {
        OnButtonBinding();

        if(Photon_Room.isInRoom)
        {
            Open();
        }
    }

    public override void OnButtonBinding()
    {
        foreach (Button _btn in buttons)
        {
            switch (_btn.name)
            {
                case "MainBtn_QuickMatch":
                    _btn.onClick.AddListener(() => {
                        Photon_Room.JoinRandomRoom();
                    });
                    break;
                case "BG_InRoomClose":
                    _btn.onClick.AddListener(() => {
                        Close(false);
                        startBtn.interactable = false;
                        Photon_Room.LeaveRoom();
                    });
                    break;
                case "Btn_Start":
                    startBtn = _btn;
                    startBtn.interactable = false;
                    _btn.onClick.AddListener(() => {
                        StartBtnAction();
                        
                    });
                    break;
                case "TeamName_Red":
                    _btn.onClick.AddListener(() => {
                        Photon_Room.SwitchTeam("Red");
                    });
                    break;
                case "TeamName_Blue":
                    _btn.onClick.AddListener(() => {
                        Photon_Room.SwitchTeam("Blue");
                    });
                    break;
            }
        }
    }

    public override void Open(bool force = false, float duration = 0.5F, Action callback = null, Action fallback = null)
    {
        base.Open(force, duration, callback, fallback);

        RoomUIUpdate();

        startBtn.interactable = true;
    }
    public override void Close(bool force = false, float duration = 0.5F, Action callback = null)
    {
        base.Close(force, duration, callback);

        
    }

    public override void DeInit()
    {
        RemoveAllPlayerInfoContent();
    }

    public override void TextSetting()
    {
        if (Photon_Room.isMasterClient)
        {
            GetTmp("Tmp_Btn_Start").text = "Start";
            startBtn.interactable = true;
        }
        else
        {
            GetTmp("Tmp_Btn_Start").text = "Ready";
        }

        GetTmp("RoomNameTmp").text = Photon_Room.GetRoomName();
        GetTmp("MaxPlayerTmp").text = $"{Photon_Room.GetRoomCurruntPlayerCount()} / {Photon_Room.GetRoomMaxPlayer()}";
    }

    public void RoomUIUpdate()
    {
        TextSetting();

        PlayerInfoUpdate();
    }

    public static void OpenRoomUI()
    {
        UI_InRoom roomUI = UIWindowMgr.GetInstance<UI_InRoom>();
        roomUI.Open();
    }
    public static void UpdateRoomUI()
    {
        UI_InRoom roomUI = UIWindowMgr.GetInstance<UI_InRoom>();
        roomUI.RoomUIUpdate();
    }

    void PlayerInfoUpdate()
    {
        RemoveAllPlayerInfoContent();

        Photon.Realtime.Player[] players = Photon_Room.GetSortedPlayersByActorNumber();
        
        for(int i = 0; i < players.Length; i++)
        {
            Content_RoomPlayerInfo _ele = ObjectPool.Spawn<Content_RoomPlayerInfo>("Content_RoomPlayerInfo");
            if(players[i].CustomProperties["TeamType"].ToString() == "Red")
            {
                _ele.transform.SetParent(contentParent_Red);
            }
            else
            {
                _ele.transform.SetParent(contentParent_Blue);
            }
            _ele.transform.localScale = Vector3.one;
            _ele.Init(players[i]);
            playerElements.Add(_ele);
        }
    }

    void RemoveAllPlayerInfoContent()
    {
        for (int i = 0; i < playerElements.Count; i++)
        {
            playerElements[i].Recycle();
            //playerElements.Remove(playerElements[i]);
        }

        playerElements.Clear();
    }

    void StartBtnAction()
    {
        if (Photon_Room.isMasterClient)
        {
            if (Photon_Room.IsAllPlayerReady())
                PhotonMgr.controller.LoadScene(SceneKind.InGame);
            else
                NoticeMgr.AddNotice("UnReady player exist", "Wait For Other Player");
        }
        else
        {
            // Ready
            Photon_Room.GetReady();
        }
    }
}
