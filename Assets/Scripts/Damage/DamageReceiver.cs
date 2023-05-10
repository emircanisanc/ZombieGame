using UnityEngine;
using Interfaces;
using Health;
using System;

namespace Damage
{
    public class DamageReceiver : MonoBehaviour, IDamageable, ISimpleDamage
    {
        [SerializeField] private HealthBase healthBase;
        public Action OnDamage;

        public void ApplyDamage(RaycastHit hit, float baseDamage)
        {
            healthBase.Health = Math.Max(healthBase.Health - baseDamage, 0);
            GunEffect.CreateEffect(hit);
            OnDamage?.Invoke();
        }

        public void ApplyDamage(float baseDamage)
        {
            healthBase.Health = Math.Max(healthBase.Health - baseDamage, 0);
            OnDamage?.Invoke();
        }
    }
}