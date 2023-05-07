using UnityEngine;

public class UpgradeDataBase : ScriptableObject
{
    public int maxLevel;
    public UpgradeInfo[] upgradeInfos;
}

[System.Serializable]
public class UpgradeInfo
{
    public string upgradeName;
    public string description;
}