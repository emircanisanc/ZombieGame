using UnityEngine;

[CreateAssetMenu(menuName ="Upgrade/New HealUpgradeData")]
public class HealUpgradeData: UpgradeDataBase
{
    public HealData[] healDatas;
}


[System.Serializable]
public class HealData
{
    public float healAmount;
    public float healDelay;
}