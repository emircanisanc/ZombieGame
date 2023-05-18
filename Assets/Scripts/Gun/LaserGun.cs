using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : WeaponBase
{
    void Update()
    {
        if(Shoot)
        {
            if(lastFireTime + dataGun.FireRate <= Time.timeSinceLevelLoad)
            {
                if(!isReloading)
                {
                    Fire();
                }
                
            }
        }
    }

    protected void Fire()
    {
        OnFired();

        Vector3 start = firePoint.position;
        Vector3 dir = firePoint.forward;
        if(dataGun.BulletPerShoot == 1)
        {
            Vector3 end = CalculateEndPos(start, dir);
            var hits = Physics.RaycastAll(rayStart.position, dir, dataGun.Range, dataGun.whatIsHitable);
            Array.Sort(hits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));
            HandleHit(hits);
            HandleTrail(start, dir, end);
        }
        else
        {
            for(int i = 0; i < dataGun.BulletPerShoot; i++)
            {
                dir += UnityEngine.Random.insideUnitSphere * UnityEngine.Random.Range(-0.1f, 0.1f);
                Vector3 end = CalculateEndPos(start, dir);
                var hits = Physics.RaycastAll(rayStart.position, dir, dataGun.Range, dataGun.whatIsHitable);
                Array.Sort(hits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));
                HandleHit(hits);
                HandleTrail(start, dir, end);
            }
        }   
    }
}
