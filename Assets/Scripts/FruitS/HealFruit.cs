using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;

public class HealFruit : FruitBase
{
    [SerializeField] private float healAmount = 20;

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerHealth>(out var health)) {
            health.Health += healAmount;
        }
        base.OnTriggerEnter(other);
    }
}
