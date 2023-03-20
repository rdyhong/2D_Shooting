using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Address type
public enum PoolType
{
    None = -1,

    Projectile,

    WallHitEffect,
    BodyHitEffect,
}

public partial class PoolManager : Singleton<PoolManager>
{
    public Transform PoolParent
    {
        get
        {
            return transform;
        }
    }

    // Link with Addressable Address
    public static readonly Dictionary<PoolType, string> poolName = new Dictionary<PoolType, string>
    {
        { PoolType.Projectile, "Projectile" },
        { PoolType.WallHitEffect, "WallHitEffect" },
        { PoolType.BodyHitEffect, "BodyHitEffect" },
    };

    public void Init()
    {
        StartCoroutine(CoreLoadCycle());

        // Craate Pool
        CreatePool(PoolType.Projectile, 50, PoolParent);

        // ...
    }

    public GameObject SpawnObject(PoolType _type, Vector3 pos, Quaternion rot)
    {
        string name = poolName[_type];
        if (!pooledObj.ContainsKey(name))
        {
            Debug.LogError($"Can't find {name} Pool");
            return null;
        }

        GameObject obj = pooledObj[name].Dequeue();
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        usingPooledObj[name].Add(obj);

        return obj;
    }

    public void RecycleObj(GameObject obj)
    {
        string _name = obj.name;
        _name = _name.Replace("(Clone)", "");
        if (!pooledObj[_name].Contains(obj))
        {
            pooledObj[_name].Enqueue(obj);
            obj.SetActive(false);
        }
        else
        {
            Debug.LogError($"Already contain in pool ::: {_name}");
        }
    }

}




