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

    private void Awake()
    {
        PhotonMgr.ingame = this;
    }

    private void Start()
    {
        SpawnPlayer(Vector3.zero, Quaternion.identity);
    }

    public static void SpawnPlayer(Vector3 _pos, Quaternion _rot)
    {
        PhotonNetwork.Instantiate("Photon_Resource/Player", _pos, _rot);
    }

    public void AddWeaponList(Item_Base _weapon)
    {
        allWeapons.Add(_weapon);
    }

}
