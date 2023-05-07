using UnityEngine;

[CreateAssetMenu(menuName ="Upgrade/New MeteorUpgradeData")]
public class MeteorUpgradeData: UpgradeDataBase
{
    public MeteorData[] values;
}

[System.Serializable]
public class MeteorData
{
    public float damage;
    public float spawnMeteorDelay;
    public int meteorCount;
}