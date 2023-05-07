using UnityEngine;
using Interfaces;

namespace Health
{
    public abstract class HealthBase : MonoBehaviour, IHealth
    {
        public abstract float Health {get; set;}
        [SerializeField] protected Float maxHealth;

        public bool IsDead{get; protected set;}

        protected virtual void OnEnable()
        {
            IsDead = false;
            Health = maxHealth.Value;
        }

        public float CurrentHealth() => Health;
    }
}