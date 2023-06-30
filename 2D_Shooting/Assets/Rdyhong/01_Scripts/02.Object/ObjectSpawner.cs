using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<Transform> redSpawnPos;
    public List<Transform> blueSpawnPos;

    private void Awake()
    {
        Photon_Ingame.objectSpawner = this;
    }
}
