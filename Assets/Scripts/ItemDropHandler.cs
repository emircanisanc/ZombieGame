using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropHandler : MonoBehaviour
{
    [SerializeField] private ItemDropData itemDropData;

    public void Drop() {
        Vector3 randomness = Vector3.zero;
        foreach(var item in itemDropData.dropItems) {
            if (item.chance >= Random.Range(0, 1)) {
                randomness = randomness + new Vector3(Random.Range(0.5f, 1), 0, Random.Range(0.5f, 1));
                Vector3 pos = transform.position + randomness;
                pos.y = pos.y + 0.7f;
                FruitSpawner.SpawnFruitAt(item.type, pos);
            }
        }
    }

}