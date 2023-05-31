using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(PoolManager))]
public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public class SpawnData
    {
        public int phase;
        public float phaseDuration;
        public float delayAfterPhase;
        public SpawnType spawnType;
        public List<SpawnEnemyData> spawnEnemyDatas;
    }

    [Serializable]
    public class SpawnEnemyData
    {
        public ObjType enemyType;
        public float spawnDelay;
        [HideInInspector] public float spawnTimer;
    }

    public enum SpawnType
    {
        Unit,
        Boss
    }

    private PoolManager poolManager;
    [SerializeField] private List<SpawnData> spawnDatas;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private float positionRandomness = 2f;
    [SerializeField] private TextMeshProUGUI waveText;

    private int currentPhaseIndex;
    private float phaseTimer;
    private float waitTimer;
    private bool isPhaseStarted;
    private bool canSpawn;

    private bool allEnemiesSpawned;
    private int deathCounter;
    private int spawnedCounter;

    public static Action AllEnemiesDied;

    private void Start()
    {
        poolManager = GetComponent<PoolManager>();
        canSpawn = true;
        EnemyBaseAbstract.AnEnemyDied += CheckEnemyState;
    }

    private void OnDestroy()
    {
        EnemyBaseAbstract.AnEnemyDied -= CheckEnemyState;
    }

    private void CheckEnemyState()
    {
        deathCounter++;
        if (deathCounter >= spawnedCounter && !allEnemiesSpawned)
        {
            if (canSpawn)
                return;

            if (allEnemiesSpawned)
            {
                AllEnemiesDied?.Invoke();
            }
            else
            {
                canSpawn = true;
            }
        }
    }

    private void Update()
    {
        if (allEnemiesSpawned)
            return;

        if (currentPhaseIndex >= spawnDatas.Count)
        {
            allEnemiesSpawned = true;
            if (deathCounter == spawnedCounter)
            {
                AllEnemiesDied?.Invoke();
            }
            return;
        }

        if (!canSpawn)
            return;

        var currentPhase = spawnDatas[currentPhaseIndex];

        if (!isPhaseStarted)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= currentPhase.delayAfterPhase)
            {
                isPhaseStarted = true;
            }
            else
            {
                return;
            }
        }

        if (currentPhase.spawnType == SpawnType.Boss)
        {
            if (deathCounter == spawnedCounter)
            {
                SpawnBossEnemies(currentPhase);
                NextPhase();
                canSpawn = false;
            }
            return;
        }

        phaseTimer += Time.deltaTime;

        if (phaseTimer >= currentPhase.phaseDuration)
        {
            if ((float)deathCounter / (float)spawnedCounter >= 0.9f)
            {
                NextPhase();
            }
            return;
        }

        foreach (var enemyData in currentPhase.spawnEnemyDatas)
        {
            enemyData.spawnTimer -= Time.deltaTime;
            if (enemyData.spawnTimer <= 0)
            {
                SpawnUnitEnemy(enemyData);
                spawnedCounter++;
            }
        }
    }

    private void SpawnUnitEnemy(SpawnEnemyData enemyData)
    {
        Vector3 spawnPos = GetRandomSpawnPosition();
        var enemy = poolManager.Get(enemyData.enemyType);
        enemy.transform.SetParent(transform);
        enemy.transform.position = spawnPos;
        enemy.SetActive(true);
        enemyData.spawnTimer = enemyData.spawnDelay;
    }

    private void SpawnBossEnemies(SpawnData currentPhase)
    {
        foreach (var enemyData in currentPhase.spawnEnemyDatas)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();
            var enemy = poolManager.Get(enemyData.enemyType);
            enemy.transform.SetParent(transform);
            enemy.transform.position = spawnPos;
            enemy.SetActive(true);
            enemyData.spawnTimer = enemyData.spawnDelay;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPos = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        var randomness = UnityEngine.Random.insideUnitCircle * UnityEngine.Random.Range(-positionRandomness, positionRandomness);
        spawnPos += new Vector3(randomness.x, 0, randomness.y);
        return spawnPos;
    }

    private void NextPhase()
    {
        currentPhaseIndex++;
        waveText.SetText("WAVE " + (currentPhaseIndex + 1).ToString());
        phaseTimer = 0;
        isPhaseStarted = false;
    }
}
