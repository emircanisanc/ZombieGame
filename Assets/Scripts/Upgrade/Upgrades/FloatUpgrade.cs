using UnityEngine;

public class FloatUpgrade : UpgradeBase
{
    [SerializeField] protected Float floatToChange;
    [SerializeField] protected FloatUpgradeData upgradeData;

    public override UpgradeDataBase UpgradeData => upgradeData;

    protected override void OnLevelUp()
    {
        floatToChange.Value = upgradeData.values[level];
    }
}
