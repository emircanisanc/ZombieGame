using Interfaces;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Float radius;
    [SerializeField] private Float damage;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject gfx;
    [SerializeField] private GameObject explosionParticle;
    private Rigidbody rg;
    private Vector3 dir;
    private bool isActive;

    RaycastHit[] hits;

    void Awake()
    {
        rg = GetComponent<Rigidbody>();
        hits = new RaycastHit[15];
    }

    public void SetRocketActive(Vector3 targetPos)
    {
        // Face the rocket towards the target
        dir = (targetPos - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir);

        // Activate the rocket and set its state to active
        gfx.SetActive(true);
        explosionParticle.SetActive(false);
        gameObject.SetActive(true);
        isActive = true;
    }


    void Update()
    {
        if (!isActive)
            return;

        rg.velocity = dir * moveSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive)
            return;
        if (other.CompareTag("Enemy") || other.CompareTag("Ground"))
        {
            isActive = false;
            dir = Vector3.zero;
            rg.velocity = Vector3.zero;
            gfx.SetActive(false);
            explosionParticle.SetActive(true);
            ApplyDamageToArea();
        }
    }

    private void ApplyDamageToArea()
    {
        // check area
        if(Physics.SphereCastNonAlloc(transform.position, radius.Value, transform.forward, hits,  0f, enemyLayer) > 0) {
            foreach(var hit in hits)
            {
                if(!hit.collider)
                    continue;
                if(hit.collider.TryGetComponent<ISimpleDamage>(out var  damageable))
                {
                    damageable.ApplyDamage(damage.Value);
                }
            }
        }
        Invoke("SetDisabled", 1.5f);
    }

    private void SetDisabled()
    {
        gameObject.SetActive(false);
    }

}
