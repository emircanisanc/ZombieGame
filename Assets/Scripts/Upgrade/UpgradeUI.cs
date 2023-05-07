using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public Action OnUpgradeSelected;
    public static Action OnAnyUpgradeSelected;

    [SerializeField] private TextMeshProUGUI nameTexT;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void SelectUpgrade()
    {
        OnUpgradeSelected?.Invoke();
        OnAnyUpgradeSelected?.Invoke();
    }

    public void Show(int level, UpgradeInfo upgradeInfo)
    {
        gameObject.SetActive(true);
        nameTexT.text = upgradeInfo.upgradeName;
        descriptionText.text = upgradeInfo.description;
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
