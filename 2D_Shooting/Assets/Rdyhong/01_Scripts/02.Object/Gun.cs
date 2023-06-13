using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunState
{
    None = -1,

    Ground,
    Equiped,
    Reloading,
}

public class Gun : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCol;
    [SerializeField] CircleCollider2D onGroundTrigger;

    [SerializeField] GameObject ModelObj_Equip;
    [SerializeField] GameObject ModelObj_Ground;

    [SerializeField] GunState state = GunState.None;

    GunData gunData = new GunData();

    protected Transform firePos;
    protected bool isOnHand = false;

    protected float curruntDelay;

    bool isMine = false;

    private void Awake()
    {
        firePos = transform.Find("FirePos");

        // Set temp data

        gunData.delay = 0.1f;
    }

    public void Init(GunData _data)
    {
        gunData = _data;
    }

    void Update()
    {
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

        curruntDelay = gunData.delay;

        Projectile projectile = ObjectPool.Spawn<Projectile>("Projectile", firePos.position);
        //projectile.transform.position = firePos.position;
        projectile.transform.rotation = firePos.rotation;

        AmmoData projectileData = new AmmoData();

        // Set Temp Data
        projectileData.speed = 180;
        projectileData.damage = 10;
        projectileData.thickness = 0.1f;
        projectileData.name = "9mm";

        projectile.SetProjectile(projectileData, isMine);

        CameraController.CamShotEffect(2);
    }

    public Gun Equip(Transform _parent, bool _isMine)
    {
        isMine = _isMine;

        transform.SetParent(_parent);
        transform.localPosition = Vector3.zero;

        return this;
    }

    public void Drop(Vector3 _dropDir)
    {
        transform.SetParent(null);
        
    }
}
