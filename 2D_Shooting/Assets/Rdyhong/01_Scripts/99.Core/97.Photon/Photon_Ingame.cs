using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Photon_Ingame : MonoBehaviourPun
{
    public List<Item_Base> allWeapons = new List<Item_Base>();

    public static ObjectSpawner objectSpawner;

    private void Awake()
    {
        PhotonMgr.ingame = this;
    }

    private void Start()
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "InGame_Join", true } });

        if (PhotonNetwork.IsMasterClient)
            StartCoroutine(IngameInitSequence_Master());

        StartCoroutine(IngameInitSequence());
    }

    IEnumerator IngameInitSequence_Master()
    {
        while (true)
        {
            yield return null;

            bool isAllReady = true;

            for (int i = 0; i < Photon_Room.GetAllPlayer().Length; i++)
            {
                if (!(bool)Photon_Room.GetAllPlayer()[i].CustomProperties["InGame_Join"])
                {
                    isAllReady = false;
                    break;
                }
            }

            if (isAllReady) break;
        }

        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable { { "InGame_Start", true } });
    }

    IEnumerator IngameInitSequence()
    {
        WaitUntil _waitUntil = new WaitUntil(() => (bool)PhotonNetwork.CurrentRoom.CustomProperties["InGame_Start"]);

        yield return _waitUntil;

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Transform _spawnTarget;

        if(PhotonNetwork.LocalPlayer.CustomProperties["TeamType"].ToString() == "Red")
        {
            _spawnTarget = objectSpawner.redSpawnPos[Photon_Room.GetMyOrderInMyTeam()];
        }
        else if(PhotonNetwork.LocalPlayer.CustomProperties["TeamType"].ToString() == "Blue")
        {
            _spawnTarget = objectSpawner.blueSpawnPos[Photon_Room.GetMyOrderInMyTeam()];
        }
        else
        {
            _spawnTarget = null;

            DebugMgr.LogErr("Spawn Target Error [NULL]");
            return;
        }

        PhotonNetwork.Instantiate("Photon_Resource/Player", _spawnTarget.position, _spawnTarget.rotation);
    }

    public void AddWeaponList(Item_Base _weapon)
    {
        allWeapons.Add(_weapon);
    }

}
