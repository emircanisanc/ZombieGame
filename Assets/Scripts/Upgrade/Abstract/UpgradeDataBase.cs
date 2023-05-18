using UnityEngine;

public class UpgradeDataBase : ScriptableObject
{
    public int maxLevel;
    public Sprite icon;
    public UpgradeInfo[] upgradeInfos;
}

[System.Serializable]
public class UpgradeInfo
{
    public string upgradeName;
    public string description;
}