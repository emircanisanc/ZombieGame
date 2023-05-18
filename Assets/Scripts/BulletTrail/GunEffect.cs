using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonoPool))]
public class GunEffect : MonoBehaviour
{
    private static GunEffect instance;
    private static MonoPool gunEffectPool;
    
    void Awake()
    {
        instance = this;
        gunEffectPool = GetComponent<MonoPool>();
    }

    public static void CreateEffect(Vector3 hitPoint, Vector3 hitNormal)
    {
        var effect = gunEffectPool.Get();
        effect.transform.position = hitPoint;
        effect.transform.eulerAngles = hitNormal;
        effect.SetActive(true);
    }
}
