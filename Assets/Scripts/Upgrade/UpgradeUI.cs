using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Action OnUpgradeSelected;
    public static Action OnAnyUpgradeSelected;

    [SerializeField] private TextMeshProUGUI nameTexT;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image icon;

    public void SelectUpgrade()
    {
        OnUpgradeSelected?.Invoke();
        OnAnyUpgradeSelected?.Invoke();
    }

    public void Show(int level, UpgradeInfo upgradeInfo, Sprite icon)
    {
        gameObject.SetActive(true);
        nameTexT.text = upgradeInfo.upgradeName;
        descriptionText.text = upgradeInfo.description;
        this.icon.sprite = icon;
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
