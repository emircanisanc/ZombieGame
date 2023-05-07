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

    public static void CreateEffect(RaycastHit hit)
    {
        var effect = gunEffectPool.Get();
        effect.transform.position = hit.point;
        effect.transform.eulerAngles = hit.normal;
        effect.SetActive(true);
    }
}
