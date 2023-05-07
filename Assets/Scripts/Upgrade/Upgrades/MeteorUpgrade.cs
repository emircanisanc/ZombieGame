using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonoPool))]
public class MeteorUpgrade : UpgradeBase
{
    [SerializeField] private float radius = 2f;
    [SerializeField] private MeteorUpgradeData meteorUpgradeData;
    public override UpgradeDataBase UpgradeData => meteorUpgradeData;
    private MonoPool meteorPool;

    [Header("Meteor Data")]
    [SerializeField] private Float meteorDamageToChange;

    private int meteorCount;
    private float spawnMeteorDelay;
    private float nextSpawnTime;
    private bool isEnable = false;

    void Awake()
    {
        meteorPool = GetComponent<MonoPool>();
    }

    protected override void OnLevelUp()
    {
        spawnMeteorDelay = meteorUpgradeData.values[level].spawnMeteorDelay;
        meteorCount = meteorUpgradeData.values[level].meteorCount;
        meteorDamageToChange.Value = meteorUpgradeData.values[level].damage;
        if (level > 0)
        {
            isEnable = true;
            nextSpawnTime = Time.timeSinceLevelLoad + spawnMeteorDelay;
        }
    }

    void Update()
    {
        if (!isEnable)
            return;
        if (Time.timeSinceLevelLoad >= nextSpawnTime)
        {
            nextSpawnTime = Time.timeSinceLevelLoad + spawnMeteorDelay;
            SpawnMeteors();
        }
    }

    private void SpawnMeteors()
    {
        float angleIncrement = 360 / meteorCount;
        float angle = Random.Range(0, 360);

        for (int i = 0; i < meteorCount; i++)
        {
            Vector3 randomPos;

            var direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));

            randomPos = transform.position + direction * radius;

            var meteor = meteorPool.Get().GetComponent<Meteor>();
            meteor.SetMeteorActive(randomPos);

            angle += angleIncrement;
        }
    }


}
