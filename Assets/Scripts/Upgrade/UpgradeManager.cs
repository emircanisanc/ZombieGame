using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Health;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private UpgradeArea upgradeArea;

    public static Action OnUpgradeMenuClosed;

    private List<UpgradeBase> upgrades;
    [SerializeField] private int maxUpgradeOption = 3;

    void Awake()
    {
        UpgradeArea.OnSafeAreaEntered += ShowUpgradeMenu;
        UpgradeUI.OnAnyUpgradeSelected += CloseUpgradeMenu;
        PlayerHealth.OnPlayerDie += DeactiveUpgrades;
        
        upgrades = new List<UpgradeBase>(GetComponentsInChildren<UpgradeBase>());
    }
    void OnDestroy()
    {
        UpgradeArea.OnSafeAreaEntered -= ShowUpgradeMenu;
        UpgradeUI.OnAnyUpgradeSelected -= CloseUpgradeMenu;
        PlayerHealth.OnPlayerDie -= DeactiveUpgrades;
    }
    private void ShowUpgradeMenu()
    {
        List<UpgradeBase> availableUpgrades = new List<UpgradeBase>(upgrades.FindAll(x => x.CanLevelUpMore));
        int numUpgradesToShow = Mathf.Min(maxUpgradeOption, availableUpgrades.Count);
        List<UpgradeBase> selectedUpgrades = new List<UpgradeBase>();
        while (selectedUpgrades.Count < numUpgradesToShow)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableUpgrades.Count);
            UpgradeBase selectedUpgrade = availableUpgrades[randomIndex];
            if (!selectedUpgrades.Contains(selectedUpgrade))
            {
                selectedUpgrades.Add(selectedUpgrade);
            }
        }

        foreach (var upgrade in selectedUpgrades)
        {
            upgrade.ShowUI();
        }
        upgradePanel.SetActive(true);
    }
    private void CloseUpgradeMenu()
    {
        foreach(var upgrade in upgrades)
            upgrade.CloseUI();
        upgradePanel.SetActive(false);
        OnUpgradeMenuClosed?.Invoke();
    }
    private void DeactiveUpgrades()
    {
        gameObject.SetActive(false);
    }
}
