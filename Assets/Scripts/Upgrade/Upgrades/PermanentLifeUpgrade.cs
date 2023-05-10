using UnityEngine;

namespace Upgrade
{
    public class PermanentLifeUpgrade : UpgradeBase
    {
        [SerializeField] private PermanentHealUpgradeData healUpgradeData;
        [SerializeField] protected Float currentHealth;
        [SerializeField] protected Float maxHealth;

        public override UpgradeDataBase UpgradeData => healUpgradeData;

        private float healAmount;

        protected override void OnLevelUp()
        {
            healAmount = healUpgradeData.extraHealValues[level];
            Heal();
        }
        private void Heal()
        {
            currentHealth.Value += healAmount;
            maxHealth.Value += healAmount;
        }
    }
}