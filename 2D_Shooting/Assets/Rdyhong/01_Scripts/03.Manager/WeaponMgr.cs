using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMgr : Singleton<WeaponMgr>
{
    public static List<Weapon_Base> allWeapons = new List<Weapon_Base>();

    protected override void Awake()
    {
        base.Awake();
    }

    public static void AddWeaponList(Weapon_Base _weapon)
    {
        _weapon.idx = allWeapons.Count;
        allWeapons.Add(_weapon);
    }
}
