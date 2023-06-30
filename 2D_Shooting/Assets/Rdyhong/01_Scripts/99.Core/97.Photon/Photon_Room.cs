using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public partial class Photon_Room : MonoBehaviourPunCallbacks
{
    public static bool isInGame = false;
    private void Awake()
    {
        PhotonMgr.room = this;
    }

    public static bool isInRoom { get { return PhotonNetwork.InRoom; } }
    public static bool isMasterClient { get { return PhotonNetwork.IsMasterClient; } }

    public static void ResetCustomProperties()
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "Player_Index", -1 } });
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "TeamType", "Red" } });
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "Room_Ready", false } });
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "InGame_Join", false } });
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "InGame_Start", false } });
    }

    public static void CreatedRoom(string _roomName = null, RoomOptions _option = null, Action _callback = null)
    {
        PhotonMgr.OnWorkingBlock();

        createRoomCB = _callback;

        PhotonNetwork.CreateRoom(_roomName, _option, null);
    }

    public static void JoinRoom(string _roomName = null)
    {
        if (PhotonNetwork.InRoom) return;

        PhotonMgr.OnWorkingBlock();

        foreach(RoomInfo _info in GetAllRoomInfo())
        {
            if(_info.Name == _roomName)
            {
                PhotonNetwork.JoinRoom(_roomName);
                break;
            }
        }
        PhotonMgr.OnWorking = false;
        NoticeMgr.AddNotice($"Room is not found\n'{_roomName}'", "Room not found");
    }
    public static void JoinRandomRoom(Action _joinRandomRoomCB = null)
    {
        PhotonMgr.OnWorkingBlock();

        if (isInRoom)
        {
            NoticeMgr.AddNotice("Already In Room", "Fail");
            return;
        }

        PhotonNetwork.JoinRandomRoom();
    }

    public static RoomOptions SetRoomOption(int _maxPlayer = 4, bool _isOpen = true, bool _isVisible = true)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = _maxPlayer;
        options.IsOpen = _isOpen;
        options.IsVisible = _isVisible;
        
        return options;
    }

    public static void LeaveRoom()
    {
        PhotonMgr.OnWorkingBlock();

        if (!isInRoom)
        {
            DebugMgr.LogErr("Not In Room");
            PhotonMgr.OnWorking = false;
            return;
        }

        PhotonNetwork.LeaveRoom();
    }

    public static void GetReady()
    {
        if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["Room_Ready"] == true) return;

        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "Room_Ready", true } });
    }
    public static void SwitchTeam(string _teamName)
    {
        if(PhotonNetwork.LocalPlayer.CustomProperties["TeamType"].ToString() != _teamName)
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "TeamType", _teamName } });
    }

    public static string GetRoomName()
    {
        return PhotonNetwork.CurrentRoom.Name;
    }
    public static int GetRoomMaxPlayer()
    {
        return PhotonNetwork.CurrentRoom.MaxPlayers;
    }
    public static int GetRoomCurruntPlayerCount()
    {
        return PhotonNetwork.CurrentRoom.PlayerCount;
    }
    private static List<RoomInfo> roomList = new List<RoomInfo>();
    public static List<RoomInfo> GetAllRoomInfo()
    {
        return roomList;
    }

    public static Player[] GetAllPlayer()
    {
        return PhotonNetwork.PlayerList;
    }
    public static Player[] GetSortedPlayersByActorNumber()
    {
        Player[] _result = new Player[PhotonNetwork.PlayerList.Length];

        Player _minP = null;

        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            _minP = PhotonNetwork.PlayerList[i];

            for (int k = i + 1; k < PhotonNetwork.PlayerList.Length; k++)
            {
                if(_minP.ActorNumber > PhotonNetwork.PlayerList[k].ActorNumber)
                {
                    _minP = PhotonNetwork.PlayerList[k];
                }
            }

            _result[i] = _minP;
        }

        return _result;
    }

    public static void SetRoomPlayersIndex()
    {
        if (!isMasterClient) return;
        
        
    }

    public static bool IsAllPlayerReady()
    {
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            if (!(bool)players[i].CustomProperties["Room_Ready"])
            {
                return false;
            }
        }

        return true;
    }
    public static int GetMyOrderInMyTeam()
    {
        Player[] _players = GetSortedPlayersByActorNumber();

        string myTeamType = PhotonNetwork.LocalPlayer.CustomProperties["TeamType"].ToString();
        int _order = 0;

        for (int i = 0; i < _players.Length; i++)
        {
            if (_players[i].CustomProperties["TeamType"].ToString() == myTeamType)
            {
                if (_players[i] == PhotonNetwork.LocalPlayer)
                    return _order;
                else
                    _order++;
            }
        }

        DebugMgr.LogErr($"TeamType Property Error - [{myTeamType}]");
        return -1;
    }

    public static bool CheckGameStartCondition()
    {
        if (PhotonNetwork.CountOfPlayers < 2) return false;
        if (!IsAllPlayerReady()) return false;

        return true;
    }
}
