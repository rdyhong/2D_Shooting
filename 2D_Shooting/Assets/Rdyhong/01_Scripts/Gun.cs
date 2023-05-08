using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    GunData gunData = new GunData();

    [SerializeField]
    protected Transform firePos;
    protected bool isOnHand = false;

    GunState state = GunState.Ready;

    protected float curruntDelay;

    private void Awake()
    {
        firePos = transform.Find("FirePos");


        // Set temp data

        gunData.delay = 0.15f;
    }

    public void InitData(GunData _data)
    {
        gunData = _data;
    }

    void Update()
    {
        if (InputManager.isFire)
        {
            if (curruntDelay <= 0 && state == GunState.Ready)
            {
                curruntDelay = gunData.delay;

                Fire();
            }
        }

        if(curruntDelay > 0)
        {
            curruntDelay -= TimeManager.ObjDeltaTime;
        }
        else
        {
            curruntDelay = 0;
        }
    }

    public void Fire()
    {
        Projectile projectile = 
            PoolManager.Instance.SpawnObject(
                PoolType.Projectile,
                firePos.position, firePos.rotation)?.GetComponent<Projectile>();

        AmmoData projectileData = new AmmoData();

        // Set Temp Data
        projectileData.speed = 180;
        projectileData.damage = 10;
        projectileData.thickness = 0.1f;
        projectileData.name = "9mm";

        projectile.SetProjectile(projectileData);

        CameraController.CamShotEffect(2);
    }


}
