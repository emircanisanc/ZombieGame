using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class PlayerLevelManager : MonoBehaviour
{
    [SerializeField] private Int playerLevel;
    [SerializeField] private Int playerExp;
    [SerializeField] private Int targetExp;
    [SerializeField] private int[] targetExps;

    [SerializeField] private UpgradeArea area;
    [SerializeField] private GameObject levelUpParticle;
    private bool canGainExp = true;
    void Awake()
    {
        playerExp.Value = 0;
        playerLevel.Value = 0;
        targetExp.Value = targetExps[playerLevel.Value];
        playerExp.OnValueChanged += CheckLevelUp;
        UpgradeArea.OnSafeAreaEntered += LevelUp;
        EnemyBaseAbstract.OnEnemyDied += AddExpOnEnemyKilled;
    }
    void OnDestroy()
    {
        playerExp.OnValueChanged -= CheckLevelUp;
        UpgradeArea.OnSafeAreaEntered -= LevelUp;
        EnemyBaseAbstract.OnEnemyDied -= AddExpOnEnemyKilled;
    }

    private void AddExpOnEnemyKilled(GameObject enemy)
    {
        if(!canGainExp)
            return;
        if(enemy.TryGetComponent<IKillable>(out var killable))
        {
            playerExp.Value += killable.ExperienceOnDie();
        }
    }

    private void LevelUp()
    {
        playerLevel.Value += 1;
        if(targetExps.Length != playerLevel.Value)
        {
            targetExp.Value = targetExps[playerLevel.Value];
            playerExp.Value = 0;
            canGainExp = true;
        }
    }

    private void CheckLevelUp(int currentExp)
    {
        if(currentExp >= targetExps[playerLevel.Value] && canGainExp)
        {
            if(area.TryEnableAtRandomPosition())
            {
                canGainExp = false;
                levelUpParticle.SetActive(true);
                Invoke("DeactiveParticle", 1.5f);
            }
        }
    }
    private void DeactiveParticle()
    {
        levelUpParticle.SetActive(false);
    }
}
