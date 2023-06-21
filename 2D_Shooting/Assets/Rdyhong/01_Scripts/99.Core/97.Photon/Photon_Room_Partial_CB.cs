using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public partial class Photon_Room : MonoBehaviourPunCallbacks
{
    public static Action createRoomCB = null;
    public static Action joinRoomCB = null;

    public override void OnCreatedRoom()
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "Room_Ready", true } });

        createRoomCB?.Invoke();
    }
    public override void OnJoinedRoom()
    {
        joinRoomCB?.Invoke();

        UI_InRoom.OpenRoomUI();

    }
    public override void OnLeftRoom()
    {
        ResetLocalCustomProperties();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (isMasterClient)
        {
            newPlayer.SetCustomProperties(new Hashtable { { "Room_Ready", false } });

            // Team property Setting
            int redCount = 0;
            int blueCount = 0;
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (PhotonNetwork.PlayerList[i] == newPlayer) continue; // Skip if newPlayer

                if (PhotonNetwork.PlayerList[i].CustomProperties["TeamType"].ToString() == "Red")
                    redCount++;
                else
                    blueCount++;
            }
            // if player count is more than 2
            if (PhotonNetwork.CountOfPlayers > 1)
            {
                if (redCount <= blueCount)
                {
                    newPlayer.SetCustomProperties(new Hashtable { { "TeamType", "Red" } });
                }
                else
                {
                    newPlayer.SetCustomProperties(new Hashtable { { "TeamType", "Blue" } });
                }
            }
        }

        UI_InRoom.UpdateRoomUI();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UI_InRoom.UpdateRoomUI();
    }

    public override void OnRoomListUpdate(List<RoomInfo> _roomList)
    {
        roomList.Clear();
        roomList = _roomList;
    }
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        UI_InRoom.UpdateRoomUI();
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        UI_InRoom.UpdateRoomUI();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {

    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
    }
}
