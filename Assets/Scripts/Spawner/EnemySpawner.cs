using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(PoolManager))]
public class EnemySpawner : MonoBehaviour
{

    # region Classes
    [System.Serializable]
    public class SpawnData
    {
        public int phase;
        public float phaseDuration;
        public float delayAfterPhase;
        public List<SpawnEnemyData> spawnEnemyDatas;
    }
    [System.Serializable]
    public class SpawnEnemyData
    {
        public ObjType enemyType;
        [HideInInspector] public float spawnTimer;
        public float spawnDelay;
    }
    # endregion

    private PoolManager poolManager;
    [SerializeField] private List<SpawnData> spawnDatas;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private float positionRandomness = 2f;
    private int currentPhaseIndex;
    private float phaseTimer;
    private float waitTimer;
    private bool isPhaseStarted;
    private bool canSpawn;

    private bool allEnemiesSpawned;
    private int deathCounter;
    private int spawnedCounter;

    public static Action AllEnemiesDied;

    void Start()
    {
        poolManager = GetComponent<PoolManager>();
        canSpawn = true;
        EnemyBaseAbstract.AnEnemyDied += CheckEnemyState;
    }

    private void CheckEnemyState()
    {
        deathCounter++;
        if(!allEnemiesSpawned)
            return;
        if(deathCounter == spawnedCounter)
            AllEnemiesDied?.Invoke();
    }

    void Update()
    {
        if(!canSpawn)
            return;

        if(allEnemiesSpawned)
            return;

        if (currentPhaseIndex >= spawnDatas.Count)
        {
            allEnemiesSpawned = true;
            return;
        }
        
        var currentPhase = spawnDatas[currentPhaseIndex];

        if(!isPhaseStarted)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer >= currentPhase.delayAfterPhase)
                isPhaseStarted = true;
            else
                return;
        }

        phaseTimer += Time.deltaTime;
        
        if (phaseTimer >= currentPhase.phaseDuration)
        {
            currentPhaseIndex++;
            phaseTimer = 0;
            isPhaseStarted = false;
            return;
        }
        
        foreach (var enemyData in currentPhase.spawnEnemyDatas)
        {
            enemyData.spawnTimer -= Time.deltaTime;
            
            if (enemyData.spawnTimer <= 0)
            {

                Vector3 spawnPos = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
                var randomness = UnityEngine.Random.insideUnitCircle * UnityEngine.Random.Range(-positionRandomness, positionRandomness);
                spawnPos += new Vector3(randomness.x, 0, randomness.y);
                var enemy = poolManager.Get(enemyData.enemyType);
                enemy.transform.SetParent(transform);
                enemy.transform.position = spawnPos;
                enemy.SetActive(true);
                enemyData.spawnTimer = enemyData.spawnDelay;
                spawnedCounter++;
            }
        }
    }
}


