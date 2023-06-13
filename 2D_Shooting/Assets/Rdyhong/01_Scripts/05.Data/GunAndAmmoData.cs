using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAndAmmoData : Singleton<GunAndAmmoData>
{
    public static Dictionary<GunType, GunData> gunData = new Dictionary<GunType, GunData>();
    public static Dictionary<MagType, MagData> magData = new Dictionary<MagType, MagData>();
    public static Dictionary<AmmoType, AmmoData> ammoData = new Dictionary<AmmoType, AmmoData>();

    protected override void Awake()
    {
        base.Awake();

        InitData();
    }

    void InitData()
    {
        // GunData Init
        for(int i = 0; i < (int)GunType.Max; i++)
        {
            int idx = i + 1;
            GunData _data = new GunData();
            _data.name = $"GunName{i}";
            _data.iconPath = "";
            _data.delay = idx * 0.1f;
            _data.range = idx * 10;
            _data.isAmmoEmpty = true;
            
            gunData[(GunType)i] = _data;
        }

        // AmmoData Init
        for (int i = 0; i < (int)GunType.Max; i++)
        {
            int idx = i + 1;
            AmmoData _data = new AmmoData();
            _data.name = $"AmmoName{i}";
            _data.damage = idx * 10;
            _data.speed = idx * 100;
            _data.thickness = idx * 0.1f;

            ammoData[(AmmoType)i] = _data;
        }

        // MagData Init
        for (int i = 0; i < (int)GunType.Max; i++)
        {
            int idx = i + 1;
            MagData _data = new MagData();
            _data.name = $"MagName{i}";
            _data.maxAmmo = idx * 10;
            _data.curAmmo = _data.maxAmmo;
            magData[(MagType)i] = _data;
        }
    }
}

public class GunData
{
    public int idx;

    public string name;
    public string iconPath;

    public float delay;
    public float range;
    public float forcePower;

    public bool isAmmoEmpty;
    
    public MagData magData = new MagData();
}

public struct AmmoData
{
    public string name;

    public int damage;
    public int speed;
    public float thickness;
}

public class MagData
{
    public string name;

    public int maxAmmo;
    public int curAmmo;

    public bool isEmpty
    {
        get
        {
            return (curAmmo <= 0);
        }
    }
}
