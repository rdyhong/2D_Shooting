using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Base : MonoBehaviour
{
    public int idx = 0;

    protected virtual void Awake()
    {
        WeaponMgr.AddWeaponList(this);
    }

    public virtual void Equip(int _dix)
    {

    }
}
