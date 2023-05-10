using UnityEngine;

[CreateAssetMenu(menuName ="Upgrade/New SlashUpgradeData")]
public class SlashUpgradeData: UpgradeDataBase
{
    public SlashData[] slashDatas;
}


[System.Serializable]
public class SlashData
{
    public float slashDamage;
    public float slashDelay;
    public float slashRadius;
    public float slashParticleSize;
}