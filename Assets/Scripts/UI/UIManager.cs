using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    void Awake()
    {
        PlayerHealth.OnPlayerDie += ActiveLosePanel;
        EnemySpawner.AllEnemiesDied += ActiveWinPanel;
    }
    private void ActiveLosePanel()
    {
        losePanel.SetActive(true);
    }
    private void ActiveWinPanel()
    {
        winPanel.SetActive(true);
    }

    private void OnDestroy() {
        PlayerHealth.OnPlayerDie -= ActiveLosePanel;
        EnemySpawner.AllEnemiesDied -= ActiveWinPanel;
    }
}
