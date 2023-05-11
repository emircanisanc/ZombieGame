using UnityEngine;
using UnityEngine.AI;
using Interfaces;
using Health;
using System;
using System.Collections;

public abstract class EnemyBaseAbstract : MonoBehaviour, IKillable, ISlowable
{

    public static Action<GameObject> OnEnemyDied;
    public static Action AnEnemyDied;

    [SerializeField] private int exp;
    public int ExperienceOnDie() => exp;

    [SerializeField] protected float attackRange = 2f;
    [SerializeField] protected float attackRate = 2f;
    [SerializeField] protected int attackDamage = 10;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected Transform attackOriginTransform;
    [SerializeField] protected int attackAnimCount;

    private Collider coll;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected Transform player;
    protected float nextAttackTime;
    protected bool isAttacking;
    protected bool canMove;
    protected bool isDead;
    protected float moveSpeed;

    protected virtual void Awake()
    {
        coll = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = PlayerManager.Instance;
        moveSpeed = agent.speed;
    }
    void OnEnable()
    {
        isDead = false;
        coll.enabled = true;
        StartChasing();
        animator.SetFloat("MoveSpeed", moveSpeed);
        GetComponent<EnemyHealth>().OnEnemyDie += Die;
        UpgradeArea.OnSafeAreaEntered += StopChasing;
        UpgradeArea.OnSafeAreaDisabled += StartChasing;
        agent.speed = moveSpeed;
    }
    void OnDisable()
    {
        GetComponent<EnemyHealth>().OnEnemyDie -= Die;
        UpgradeArea.OnSafeAreaEntered -= StopChasing;
        UpgradeArea.OnSafeAreaDisabled -= StartChasing;
    }

    private void StopChasing()
    {
        StopMove();
        canMove = false;   
    }
    private void StartChasing()
    {
        if(isDead)
            return;
        StartMove();
        canMove = true;
    }
    protected void StopMove()
    {
        animator.SetBool("IsWalking", false);
        agent.isStopped = true;
    }
    protected void StartMove()
    {
        animator.SetBool("IsWalking", true);
        agent.isStopped = false;
    }

    protected virtual void Update()
    {
        if(!canMove)
            return;
        if (!isAttacking)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            agent.SetDestination(player.position);
            if (distanceToPlayer <= attackRange)
            {
                FaceTarget();
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    var attackAnim = "Attack" + UnityEngine.Random.Range(0, attackAnimCount);
                    animator.SetTrigger(attackAnim);
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }

    public abstract void Attack();
    protected virtual void Die()
    {
        coll.enabled = false;
        isDead = true;
        isAttacking = false;
        OnEnemyDied?.Invoke(gameObject);
        AnEnemyDied?.Invoke();
        StopAllCoroutines();
        StopChasing();
        animator.SetTrigger("Die");
        Invoke("Disappear", 2f);
    }
    private void Disappear()
    {
        gameObject.SetActive(false);
    }
    private void FaceTarget()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    protected bool isSlow;
    public void ApplySlow(float slowAmount, float slowDuration)
    {
        if(isSlow)
            return;
        StartCoroutine(SlowEffect(slowAmount, slowDuration));
    }

    IEnumerator SlowEffect(float slowAmount, float slowDuration)
    {
        isSlow = true;
        animator.SetFloat("MoveSpeed", 1f);
        agent.speed = agent.speed * (1 - slowAmount);
        yield return new WaitForSeconds(slowDuration);
        isSlow = false;
        agent.speed = moveSpeed;
        animator.SetFloat("MoveSpeed", moveSpeed);
    }
}
