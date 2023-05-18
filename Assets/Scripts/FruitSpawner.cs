using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolManager))]
public class FruitSpawner : MonoBehaviour
{
    private static  PoolManager poolManager;

    void Start()
    {
        poolManager = GetComponent<PoolManager>();
    }

    public static void SpawnFruitAt(ObjType type, Vector3 pos) {
        GameObject fruit = poolManager.Get(type);
        fruit.transform.position = pos;
        fruit.SetActive(true);
    }

}
