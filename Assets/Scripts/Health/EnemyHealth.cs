using System;

namespace Health
{
    public class EnemyHealth : HealthBase
    {
        private float currentHealth;

        public Action OnEnemyDie;

        public override float Health { 
            get => currentHealth; 
            set { 
                currentHealth = value; 
                if ( currentHealth <= 0 && !IsDead ) {
                    IsDead = true;
                    OnEnemyDie?.Invoke(); 
                }
                    } }
    }
}
