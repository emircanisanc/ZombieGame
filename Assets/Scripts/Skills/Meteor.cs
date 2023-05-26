using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float damageDelay = 0.5f;
    [SerializeField] private float totalTime = 3.6f;
    [SerializeField] private float radius = 4.3f;
    [SerializeField] private LayerMask enemyLayer;


    [SerializeField] private Float damagePerHit;

    public void SetMeteorActive(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        StartCoroutine(MeteorDamage());
    }

    IEnumerator MeteorDamage()
    {
        float timer = 0;
        while(timer <= totalTime)
        {
            yield return new WaitForSeconds(damageDelay);
            timer += damageDelay;
            var hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);
            foreach(var hit in hits)
            {
                if(hit.TryGetComponent<ISimpleDamage>(out var damageable))
                {
                    damageable.ApplyDamage(damagePerHit.Value);
                }
            }
        }
        gameObject.SetActive(false);
    }

}
