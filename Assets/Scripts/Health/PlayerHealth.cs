using UnityEngine;
using System;

namespace Health
{
    public class PlayerHealth : HealthBase
    {
        [SerializeField] private Float health;
        [SerializeField] private Float tempMaxHealth;

        void Awake()
        {
            tempMaxHealth.Value = maxHealth.Value;    
        }

        public static Action OnPlayerDie;

        public override float Health { 
            get => health.Value; 
            set { 
                health.Value = value; 
                if ( value <= 0 && !IsDead ) {
                    IsDead = true;
                    OnPlayerDie?.Invoke();
                }
                    } }
    }
}