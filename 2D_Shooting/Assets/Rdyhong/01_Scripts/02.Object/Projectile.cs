using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    TrailRenderer trailRenderer;

    AmmoData data;

    bool hited = false;
    bool isActive = false;

    bool isMine = false;

    private void Awake()
    {
        trailRenderer = transform.GetComponent<TrailRenderer>();
    }

    public void SetProjectile(AmmoData _data, bool _isMine)
    {
        isMine = _isMine;

        isActive = true;
        hited = false;

        data = _data;
    }

    void Update()
    {
        if(!isActive)
        {
            return;
        }

        if(hited)
        {
            isActive = false;
            //PoolManager.Inst.RecycleObj(this.gameObject);
            ObjectPool.Recycle(this);
            return;
        }

        MoveAndCheckHit();
    }

    private void MoveAndCheckHit()
    {
        Vector3 nextPos;
        nextPos = transform.position + (transform.up * data.speed) * TimeMgr.ObjDeltaTime;

        float distance = (nextPos - transform.position).magnitude;

        // Cast a ray straight up
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance);
        Debug.DrawRay(transform.position, transform.up * distance, Color.red);

        // If hits something
        if (hit)
        {
            hited = true;
            transform.position = new Vector3(hit.point.x, hit.point.y, 0);

            // If hit moveable obstacle
            MoveableObstacle obstacle = hit.transform.GetComponent<MoveableObstacle>();
            if(obstacle)
            {
                obstacle.HitEffect(transform.up, transform.position, 2);
            }
        }
        else
        {
            transform.position = nextPos;
        }
    }
}
