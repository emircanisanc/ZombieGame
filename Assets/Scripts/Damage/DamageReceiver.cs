using UnityEngine;
using Interfaces;
using Health;
using System;
using AudioEvents;

namespace Damage
{
    public class DamageReceiver : MonoBehaviour, IDamageable, ISimpleDamage
    {
        [SerializeField] private HealthBase healthBase;
        [SerializeField] private AudioEvent audioEventOnHitDamage;

        public void ApplyDamage(RaycastHit hit, float baseDamage)
        {
            if(healthBase.Health <= 0)
                return;
            healthBase.Health = Math.Max(healthBase.Health - baseDamage, 0);
            GunEffect.CreateEffect(hit.point, hit.normal);
            audioEventOnHitDamage.Play(Audio.Instance);
        }

        public void ApplyDamage(float baseDamage)
        {
            if(healthBase.Health <= 0)
                return;
            healthBase.Health = Math.Max(healthBase.Health - baseDamage, 0);
        }
    }
}