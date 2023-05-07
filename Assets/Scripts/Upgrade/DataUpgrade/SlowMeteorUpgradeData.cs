using UnityEngine;

[CreateAssetMenu(menuName ="Upgrade/New SlowMeteorUpgradeData")]
public class SlowMeteorUpgradeData: UpgradeDataBase
{
    public SlowMeteorData[] values;
}

[System.Serializable]
public class SlowMeteorData
{
    public float damage;
    [Range(0, 1)] public float slowAmount;
    public float slowDuration;

    public float spawnMeteorDelay;
    public int meteorCount;
}