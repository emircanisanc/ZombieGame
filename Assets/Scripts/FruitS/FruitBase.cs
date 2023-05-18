using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBase : MonoBehaviour
{
    [SerializeField] private FruitData fruitData;

    protected virtual void OnTriggerEnter(Collider other) {
        Collect();
        gameObject.SetActive(false);
    }

    protected virtual void Collect() {
        fruitData.playerExp.Value += fruitData.fruitExp;
    }
}
