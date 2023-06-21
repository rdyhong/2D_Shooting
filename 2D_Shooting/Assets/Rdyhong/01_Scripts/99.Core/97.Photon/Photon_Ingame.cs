using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Photon_Ingame : MonoBehaviourPun
{
    private void Start()
    {
        SpawnPlayer(Vector3.zero, Quaternion.identity);
    }

    public static void SpawnPlayer(Vector3 _pos, Quaternion _rot)
    {
        PhotonNetwork.Instantiate("Photon_Resource/Player", _pos, _rot);
    }

    public void EquipItem(int _playerIdx, int _itemIdx)
    {
        photonView.RPC("RPC_EquipItem", RpcTarget.All, _playerIdx, _itemIdx);
    }

    [PunRPC]
    void RPC_EquipItem(int _playerIdx, int _itemIdx)
    {
        

    }
}
