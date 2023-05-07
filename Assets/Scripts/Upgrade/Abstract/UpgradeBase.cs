using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeBase : MonoBehaviour
{

    # region Level
    [SerializeField] protected int level;
    public bool CanLevelUpMore => level < UpgradeData.maxLevel;
    # endregion
    
    # region Abstract
    protected abstract void OnLevelUp();
    public abstract UpgradeDataBase UpgradeData { get; }
    # endregion

    [SerializeField] private UpgradeUI upgradeUI;

    void Start()
    {
        upgradeUI.OnUpgradeSelected += LevelUp;
        OnLevelUp();
    }
    void OnDestroy()
    {
        upgradeUI.OnUpgradeSelected -= LevelUp;
    }

    private void LevelUp()
    {
        level++;
        OnLevelUp();
    }
    public void ShowUI()
    {
        upgradeUI.Show(level, UpgradeData.upgradeInfos[level]);
    }
    public void CloseUI()
    {
        upgradeUI.Close();
    }
}
