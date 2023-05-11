using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class MeleeEnemy : EnemyBaseAbstract
{
    public override void Attack()
    {
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        isAttacking = true;
        agent.velocity = Vector3.zero;
        StopMove();
        transform.LookAt(player);

        yield return new WaitForSeconds( (1/attackRate) / 2);

        var hits = Physics.RaycastAll(attackOriginTransform.position, transform.forward, attackRange, playerLayer);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.ApplyDamage(hit, attackDamage);
                    break;
                }
            }
        }

        yield return new WaitForSeconds( (1/attackRate) / 2);

        StartMove();
        isAttacking = false;
    }
}
