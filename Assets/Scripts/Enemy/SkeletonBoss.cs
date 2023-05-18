using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Health;

public class SkeletonBoss : EnemyBaseAbstract
{
    [SerializeField] private EnemyHealth health;
    private bool isRunning;
    private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float healthToStartRun = 30;

    protected override void Awake()
    {
        base.Awake();
        health = GetComponent<EnemyHealth>();
        walkSpeed = moveSpeed;
    }

    protected override void Update()
    {
        base.Update();
        if(!isRunning && health.CurrentHealth() < healthToStartRun && !isDead)
        {
            isRunning = true;
            moveSpeed = runSpeed;
            if(!isSlow)
            {
                agent.speed = moveSpeed;
                animator.SetFloat("MoveSpeed", 4f);
            }
        }
    }

    protected override void Die()
    {
        itemDropHandler.Drop();
        coll.enabled = false;
        isDead = true;
        isAttacking = false;
        AnEnemyDied?.Invoke();
        StopAllCoroutines();
        StopChasing();
        animator.SetTrigger("Die");
        Invoke("Disappear", 2f);
        isRunning = false;
        moveSpeed = walkSpeed;
    }

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
