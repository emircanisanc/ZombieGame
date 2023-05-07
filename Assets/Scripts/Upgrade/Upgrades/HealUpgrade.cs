using UnityEngine;

public class HealUpgrade : UpgradeBase
{
    [SerializeField] private HealUpgradeData healUpgradeData;
    [SerializeField] protected Float currentHealthToChange;
    [SerializeField] protected Float maxPlayerHealth;
    [SerializeField] private GameObject healParticle;
    [SerializeField] private AudioClip healClip;
    [SerializeField] private float healParticleDuration;

    public override UpgradeDataBase UpgradeData => healUpgradeData;

    private float healAmount;
    private float healDelay;
    private float nextHealthTime;
    private bool isEnable = false;
    protected override void OnLevelUp()
    {
        healAmount = healUpgradeData.healDatas[level].healAmount;
        healDelay = healUpgradeData.healDatas[level].healDelay;
        if (level > 0)
        {
            isEnable = true;
            nextHealthTime = Time.timeSinceLevelLoad + healDelay;
        }
    }

    void Update()
    {
        if (!isEnable)
            return;
        if (Time.timeSinceLevelLoad >= nextHealthTime)
        {
            nextHealthTime = Time.timeSinceLevelLoad + healDelay;
            Heal();
        }
    }
    private void Heal()
    {
        healParticle.SetActive(true);
        AudioSource.PlayClipAtPoint(healClip, transform.position);
        Invoke("SetDisableParticle", healParticleDuration);
        currentHealthToChange.Value = Mathf.Min(healAmount + currentHealthToChange.Value, maxPlayerHealth.Value);
    }
    private void SetDisableParticle()
    {
        healParticle.SetActive(false);
    }
}
