using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Index_Initialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Weapon Index Setting
        for(int i = 0; i < transform.childCount; i++)
        {
            Item_Base _weaponBase = transform.GetChild(i).GetComponent<Item_Base>();
            _weaponBase.idx = i;

            PhotonMgr.ingame.AddWeaponList(_weaponBase);
        }
    }
}
