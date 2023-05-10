using UnityEngine;

[CreateAssetMenu(menuName ="Upgrade/New NukeUpgradeData")]
public class NukeUpgradeData: UpgradeDataBase
{
    public NukeData[] values;
}

[System.Serializable]
public class NukeData
{
    public float damage;
    public float nukeRadius;
    public float spawnNukeDelay;
    public int nukeCount;
}