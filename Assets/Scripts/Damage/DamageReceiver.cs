using UnityEngine;
using Interfaces;
using Health;
using System;
using AudioEvents;

namespace Damage
{
    [RequireComponent(typeof(AudioSource))]
    public class DamageReceiver : MonoBehaviour, IDamageable, ISimpleDamage
    {
        [SerializeField] private HealthBase healthBase;
        [SerializeField] private AudioEvent audioEventOnHitDamage;
        [SerializeField] private AudioSource audioSource;
        public Action OnDamage;

        public void ApplyDamage(RaycastHit hit, float baseDamage)
        {
            healthBase.Health = Math.Max(healthBase.Health - baseDamage, 0);
            GunEffect.CreateEffect(hit);
            audioEventOnHitDamage.Play(audioSource);
            OnDamage?.Invoke();
        }

        public void ApplyDamage(float baseDamage)
        {
            healthBase.Health = Math.Max(healthBase.Health - baseDamage, 0);
            OnDamage?.Invoke();
        }
    }
}