using Health;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    [SerializeField] private WeaponBase gun;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private Animator animator;

    void Awake()
    {
        gun.OnStartFire += SetFire;
        gun.OnFire += TriggerFire;
        gun.OnStopFire += StopFire;
        gun.OnReload += SetReload;
        gun.OnReloadEnd += StopReload;
        PlayerHealth.OnPlayerDie += TriggerDie;
        UpgradeArea.OnSafeAreaEntered += StopFight;
        UpgradeArea.OnSafeAreaDisabled += StartFight;
    }

    void LateUpdate()
    {
        animator.SetFloat("Speed", playerMovement.Speed);
    }
    void OnDestroy()
    {
        gun.OnStartFire -= SetFire;
        gun.OnFire -= TriggerFire;
        gun.OnStopFire -= StopFire;
        gun.OnReload -= SetReload;
        gun.OnReloadEnd -= StopReload;
        PlayerHealth.OnPlayerDie -= TriggerDie;
        UpgradeArea.OnSafeAreaEntered -= StopFight;
        UpgradeArea.OnSafeAreaDisabled -= StartFight;
    }

    private void StartFight()
    {
        animator.SetBool("InFight", true);
    }
    private void StopFight()
    {
        animator.SetBool("InFight", false);
    }
    private void TriggerDie()
    {
        animator.SetTrigger("Die");
    }
    private void SetFire()
    {
        animator.SetBool("IsShooting", true);
    }
    private void StopFire()
    {
        animator.SetBool("IsShooting", false);
    }
    private void TriggerFire()
    {
        animator.SetTrigger("Shoot");
    }
    private void SetReload()
    {
        animator.SetBool("Reloading", true);
        animator.SetTrigger("Reload");
    }
    private void StopReload()
    {
        animator.SetBool("Reloading", false);
    }
}
