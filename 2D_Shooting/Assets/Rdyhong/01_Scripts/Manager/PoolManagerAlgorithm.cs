using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;

public partial class PoolManager : Singleton<PoolManager>
{
    Dictionary<string, Queue<GameObject>> pooledObj = new Dictionary<string, Queue<GameObject>>();
    Dictionary<string, List<GameObject>> usingPooledObj = new Dictionary<string, List<GameObject>>();

    int curLoadCount = 0;
    int targetLoadCount = 0;
    bool isReady = true;
    Queue<Action> loadQueue = new Queue<Action>();

    IEnumerator CoreLoadCycle()
    {
        while (true)
        {
            if (isReady && loadQueue.Count > 0)
            {
                isReady = false;
                loadQueue.Dequeue().Invoke();
            }
            yield return null;
        }
    }

    void CreatePool(PoolType _type, int _count = 1, Transform parent = null)
    {
        targetLoadCount += _count;

        for (int i = 0; i < _count; i++)
        {
            loadQueue.Enqueue(() => {
                LoadObj(_type, parent);
            });
        }
    }

    void LoadObj(PoolType _type, Transform parent = null)
    {
        string address = poolName[_type];
        Addressables.InstantiateAsync(address, new Vector3(0, -300, 0), Quaternion.identity).Completed += handle =>
        {
            if (!PoolManager.Instance.pooledObj.ContainsKey(address))
            {
                PoolManager.Instance.pooledObj[address] = new Queue<GameObject>();
                PoolManager.Instance.usingPooledObj[address] = new List<GameObject>();
            }

            GameObject result = handle.Result;
            result.transform.position = new Vector3(0, -300, 0);
            result.transform.eulerAngles = Vector3.zero;
            PoolManager.Instance.pooledObj[address].Enqueue(result);
            result.transform.parent = parent;
            result.SetActive(false);
            isReady = true;
            curLoadCount++;

            if(curLoadCount == targetLoadCount)
            {
                Debug.Log("Pool Load Complete");
            }
        };
    }
}
