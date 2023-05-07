using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class SlowMeteor : MonoBehaviour
{
    [SerializeField] private float slowDelay = 1.2f;
    [SerializeField] private float totalTime = 3.6f;
    [SerializeField] private float radius = 4.3f;
    [SerializeField] private LayerMask enemyLayer;

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);   
    }

    [SerializeField] private Float damagePerHit;
    [SerializeField] private Float slowAmount;
    [SerializeField] private Float slowDuration;

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
            yield return new WaitForSeconds(slowDelay);
            timer += slowDelay;
            var hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);
            foreach(var hit in hits)
            {
                if(hit.TryGetComponent<ISimpleDamage>(out var damageable))
                {
                    damageable.ApplyDamage(damagePerHit.Value);
                }
                if(hit.TryGetComponent<ISlowable>(out var slowable))
                {
                    slowable.ApplySlow(slowAmount.Value, slowDuration.Value);
                }
            }
        }
        gameObject.SetActive(false);
    }

}
