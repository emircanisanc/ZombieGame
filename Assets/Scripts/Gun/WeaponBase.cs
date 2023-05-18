using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Health;

public class WeaponBase : MonoBehaviour, IFireable, IReloadable
{
    # region IFireable
    public Action OnStartFire { get; set; }
    public Action OnFire { get; set; }
    public Action OnStopFire { get; set; }
    # endregion
    # region IReloadable
    public Action OnReload { get; set; }
    public Action OnReloadEnd { get; set; }
    # endregion


    [SerializeField] protected DataGun dataGun;

    [SerializeField] protected Transform firePoint;
    [SerializeField] protected Transform rayStart;

    protected float lastFireTime;
    [SerializeField] private Int currentAmmo;
    
    protected bool isReloading;

    private bool shoot = true;
    public bool Shoot{ get => shoot; set {
         shoot = value; if (shoot) OnStartFire?.Invoke(); else OnStopFire?.Invoke(); } }

    # region MONOBEHAVIOUR
    void Awake()
    {
        PlayerHealth.OnPlayerDie += DisableGun;
        UpgradeArea.OnSafeAreaEntered += StopFire;
        UpgradeArea.OnSafeAreaDisabled += StartFire;
    }
    void Start()
    {
        currentAmmo.Value = dataGun.AmmoSize;
    }
    void OnDestroy()
    {
        PlayerHealth.OnPlayerDie -= DisableGun;
        UpgradeArea.OnSafeAreaEntered -= StopFire;
        UpgradeArea.OnSafeAreaDisabled -= StartFire;
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }
    # endregion
    
    // APPLY DAMAGES OR OTHER STUFF FOR HIT OBJECTS
    protected void HandleHit(RaycastHit[] hits)
    {
        int hitCounter = 0;
        foreach(var hit in hits)
        {
            if(hit.collider == null)
                continue;
            if(hit.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                //GunEffect.CreateEffect(hit);
                damageable.ApplyDamage(hit, dataGun.Damage);
                hitCounter++;
                if(hitCounter == dataGun.maxHitNumber)
                    break;
            }
        }
    }
    
    // CHECK THE HIT AND RETURNS END POS OF BULLET
    protected Vector3 CalculateEndPos(Vector3 start, Vector3 dir)
    {
        Vector3 targetPos = start + dir * dataGun.Range;

        RaycastHit[] hits = Physics.RaycastAll(start, dir, dataGun.Range);
        Array.Sort(hits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));
        if(hits.Length > 0)
            targetPos = hits[0].point;

        return targetPos;
    }
    
    // HANDLES BULLET TRAIL
    protected void HandleTrail(Vector3 start, Vector3 dir, Vector3 end)
    {
        start += ( (end - start) / 2 ) * UnityEngine.Random.Range(0.4f, 0.6f);
        end += dir * UnityEngine.Random.Range(-0.2f, 1f) * dataGun.maxHitNumber;
        TrailManager.Instance.CreateTrail(start, end, dataGun.fireColor);
    }
    
    protected void OnFired()
    {
        ReduceAmmo();
        if(dataGun.fireSound)
            AudioSource.PlayClipAtPoint(dataGun.fireSound, firePoint.position);
        OnFire?.Invoke();
        lastFireTime = Time.timeSinceLevelLoad;
    }

    # region RELOAD
    protected void ReduceAmmo()
    {
        currentAmmo.Value--;
        if(currentAmmo.Value == 0)
            StartCoroutine(Reload());
    }
    IEnumerator Reload()
    {
        StopFire();
        isReloading = true;
        OnReload?.Invoke();
        yield return new WaitForSeconds(dataGun.reloadTime);
        currentAmmo.Value = dataGun.AmmoSize;
        OnReloadEnd?.Invoke();
        lastFireTime = Time.timeSinceLevelLoad;
        isReloading = false;
        yield return new WaitForSeconds(0.1f);
        StartFire();
    }
    # endregion

    private void DisableGun()
    {
        StopFire();
        enabled = false;
    }
    private void StartFire()
    {
        lastFireTime = Time.timeSinceLevelLoad;
        Shoot = true;
    }
    private void StopFire()
    {
        Shoot = false;
    }


}
