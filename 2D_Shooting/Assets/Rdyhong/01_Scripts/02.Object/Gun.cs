using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Photon.Pun;

public enum GunState
{
    None = -1,

    Reloading,
}

[Serializable]
public class GunData
{
    public string name;
    public string iconPath;

    public float shotDelay;
    public float reloadTime;

    public bool isChamberEmpty = true;

    public MagData magData = new MagData();
}

public class Gun : Item_Base
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
        

        // Set temp data
        gunData.name = "Super Power Rifle";
        gunData.shotDelay = 0.1f;

        _onFieldUI = ObjectPool.Spawn<UI_OnFieldItem>("UI_OnFieldItem");
        _onFieldUI.Init(transform, gunData.name, () => {
            InGameMgr.myCharacter.EquipItem(this.idx);
        });

        // Test
        //photonView.Owner = null;
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

        
    }

    bool CheckFireCondition()
    {
        if (curruntDelay <= 0)
        {
            return true;
        }

        return false;
    }

    public override void Use()
    {
        if (!CheckFireCondition()) return;

        base.Use();

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

    public override void Equip(CharacterController _c, Transform _parent)
    {
        transform.DOKill();

        base.Equip(_c, _parent);

        ModelObj_Equip.SetActive(true);
        ModelObj_Ground.SetActive(false);

        _onFieldUI.Active(false);

        //transform.SetParent(_parent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public override void Drop(Vector3 _dropDir)
    {
        base.Drop(_dropDir);

        ModelObj_Equip.SetActive(false);
        ModelObj_Ground.SetActive(true);

        transform.DOMove(transform.position + _dropDir, 0.7f).SetAutoKill();
        transform.DORotate(transform.rotation.eulerAngles +
            new Vector3(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z + UnityEngine.Random.Range(-20f,20f)), 0.7f);
    }

    UI_OnFieldItem _onFieldUI = null;
    void ShowUIOnGround()
    {
        if (fieldState != ItemFieldState.Ground || InGameMgr.myCharacter == null)
        {
            return; 
        }

        float _length = (InGameMgr.myCharacter.transform.position - transform.position).magnitude;

        if(_length < 1.5f)
        {
            _onFieldUI.Active(true);
        }
        else
        {
            _onFieldUI.Active(false);
        }
    }
}
