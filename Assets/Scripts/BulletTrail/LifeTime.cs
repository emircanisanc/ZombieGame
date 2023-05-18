using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.3f;

    void OnEnable()
    {
        StartCoroutine(SetDisable());
    }

    void OnDisable() {
        StopAllCoroutines();
    }

    IEnumerator SetDisable()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
