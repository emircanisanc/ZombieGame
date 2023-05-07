using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Data Assets")]
    [SerializeField] private Int currentAmmo;
    [SerializeField] private Int maxAmmo;
    [SerializeField] private Float health;
    [SerializeField] private Float maxHealth;
    [SerializeField] private Int currentExp;
    [SerializeField] private Int targetExp;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI currentAmmoText;
    [SerializeField] private TextMeshProUGUI maxAmmoText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI maxHealthText;
    [SerializeField] private Image expBar;

    void Awake()
    {
        currentAmmo.OnValueChanged += UpdateAmmo;
        maxAmmo.OnValueChanged += UpdateMaxAmmo;
        health.OnValueChanged += UpdateHealth;
        maxHealth.OnValueChanged += UpdateMaxHealth;
        currentExp.OnValueChanged += UpdateExpBar;
    }
    void OnDestroy()
    {
        currentAmmo.OnValueChanged -= UpdateAmmo;
        maxAmmo.OnValueChanged -= UpdateMaxAmmo;
        health.OnValueChanged -= UpdateHealth;
        maxHealth.OnValueChanged -= UpdateMaxHealth;
        currentExp.OnValueChanged -= UpdateExpBar;
    }

    private void UpdateAmmo(int ammo)
    {
        currentAmmoText.text = ammo.ToString();
    }
    private void UpdateMaxAmmo(int ammo)
    {
        maxAmmoText.text = ammo.ToString();
    }
    private void UpdateHealth(float health)
    {
        healthText.text = health.ToString();
    }
    private void UpdateMaxHealth(float maxHealth)
    {
        maxHealthText.text = maxHealth.ToString();
    }
    private void UpdateExpBar(int exp)
    {
        expBar.fillAmount = (float)exp / (float)targetExp.Value;
    }
    
}
