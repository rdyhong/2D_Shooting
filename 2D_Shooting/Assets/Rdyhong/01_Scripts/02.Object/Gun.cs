using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum GunState
{
    None = -1,

    Ground,
    Equiped,
    Reloading,
}

[Serializable]
public class GunData
{
    public int idx;

    public string name;
    public string iconPath;

    public float shotDelay;
    public float reloadTime;
    public float range;
    public float forcePower;

    public bool isAmmoEmpty;

    public MagData magData = new MagData();
}

public class Gun : Weapon_Base
{
    public static int weaponCount = 0;
    [SerializeField] BoxCollider2D boxCol;
    [SerializeField] CircleCollider2D onGroundTrigger;

    [SerializeField] GameObject ModelObj_Equip;
    [SerializeField] GameObject ModelObj_Ground;

    [SerializeField] GunState state = GunState.None;

    [SerializeField]
    GunData gunData = new GunData();

    protected Transform firePos;
    protected bool isEquiped = false;

    protected float curruntDelay;

    bool isMine = false;

    protected override void Awake()
    {
        base.Awake();
        firePos = transform.Find("FirePos");
        _onFieldUI = ObjectPool.Spawn<UI_OnFieldItem>("UI_OnFieldItem");
        _onFieldUI.Init(transform, "TempGunName", () => {
            InGameMgr.myCharacter.EquipItem(idx);
        });

        // Set temp data
        gunData.idx = weaponCount++;
        gunData.shotDelay = 0.1f;
    }

    public void Init(GunData _data)
    {
        gunData = _data;
    }

    void Update()
    {
        ShowUIOnGround();

        if (curruntDelay > 0)
            curruntDelay -= TimeMgr.ObjDeltaTime;

        if (InputMgr.isFire)
        {
            Fire();
        }
    }

    public void SwitchState(GunState _state)
    {
        state = _state;

        switch(state)
        {
            case GunState.Ground:
                ModelObj_Equip.SetActive(false);
                ModelObj_Ground.SetActive(true);
                break;

            case GunState.Equiped:
                ModelObj_Equip.SetActive(true);
                ModelObj_Ground.SetActive(false);
                break;

            case GunState.Reloading:

                break;
        }
    }

    bool CheckFireCondition()
    {
        if (curruntDelay <= 0 && state == GunState.Equiped)
        {
            return true;
        }

        return false;
    }

    public void Fire()
    {
        if (!CheckFireCondition()) return;

        curruntDelay = gunData.shotDelay;

        Projectile projectile = ObjectPool.Spawn<Projectile>("Projectile", firePos.position);
        //projectile.transform.position = firePos.position;
        projectile.transform.rotation = firePos.rotation;

        AmmoData projectileData = new AmmoData();

        // Set Temp Data
        projectileData.speed = 180;
        projectileData.damage = 10;
        projectileData.thickness = 0.1f;
        projectileData.name = "9mm";
        // --------------

        projectile.SetProjectile(projectileData, isMine);

        CameraController.CamShotEffect(2);
    }

    public override void Equip(int idx)
    {
        //isMine = _isMine;

        SwitchState(GunState.Equiped);
        
        _onFieldUI.Active(false);

        //transform.SetParent(_parent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Drop(Vector3 _dropDir)
    {
        transform.SetParent(null);
        transform.DOMove(transform.position + _dropDir, 0.7f).SetAutoKill();
        transform.DORotate(transform.rotation.eulerAngles +
            new Vector3(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z + UnityEngine.Random.Range(-20f,20f)), 0.7f);
        SwitchState(GunState.Ground);

    }

    UI_OnFieldItem _onFieldUI = null;
    void ShowUIOnGround()
    {
        if (state != GunState.Ground || InGameMgr.myCharacter == null)
        {
            return; 
        }

        float _length = (InGameMgr.myCharacter.transform.position - transform.position).magnitude;
        if(_length < 1)
        {
            _onFieldUI.Active(true);
        }
        else
        {
            _onFieldUI.Active(false);
        }
    }
}
