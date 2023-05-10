using UnityEngine;

namespace Upgrade
{
    public class IntUpgrade : UpgradeBase
    {
        [SerializeField] protected Int intToChange;
        [SerializeField] protected IntUpgradeData upgradeData;

        public override UpgradeDataBase UpgradeData => upgradeData;

        protected override void OnLevelUp()
        {
            intToChange.Value = upgradeData.values[level];
        }
    }
}