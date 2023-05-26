using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrade
{
    [RequireComponent(typeof(MonoPool))]
    public class NukeUpgrade : UpgradeBase
    {
        [SerializeField] private float radius = 4f;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private NukeUpgradeData nukeUpgradeData;
        public override UpgradeDataBase UpgradeData => nukeUpgradeData;
        private MonoPool nukePool;

        [Header("Nuke Data")]
        [SerializeField] private Float nukeDamageToChange;
        [SerializeField] private Float nukeRadiusToChange;

        private int nukeCount;
        private float spawnNukeDelay;
        private float nextSpawnTime;
        private bool isEnable = false;

        void Awake()
        {
            nukePool = GetComponent<MonoPool>();
        }

        protected override void OnLevelUp()
        {
            spawnNukeDelay = nukeUpgradeData.values[level].spawnNukeDelay;
            nukeCount = nukeUpgradeData.values[level].nukeCount;
            nukeDamageToChange.Value = nukeUpgradeData.values[level].damage;
            nukeRadiusToChange.Value = nukeUpgradeData.values[level].nukeRadius;
            if (level > 0)
            {
                isEnable = true;
                nextSpawnTime = Time.timeSinceLevelLoad + spawnNukeDelay;
            }
        }

        void FixedUpdate()
        {
            if (!isEnable)
                return;
            if (Time.timeSinceLevelLoad >= nextSpawnTime)
            {
                if(SpawnNukes())
                    nextSpawnTime = Time.timeSinceLevelLoad + spawnNukeDelay;
            }
        }

        private bool SpawnNukes()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.forward, 0f, enemyLayer);
            int counter = 0;
            if(hits.Length == 0)
                return false;
            for(int i = 0; i < hits.Length; i++)
            {
                Vector3 startPos = transform.position + new Vector3(0, 10f, 0) + Random.insideUnitSphere * 2f;
                var nuke = nukePool.Get().GetComponent<Rocket>();
                nuke.transform.position = startPos;
                nuke.SetRocketActive(hits[i].transform.position);
                counter++;
                if(counter == nukeCount)
                    break;
                else if(i == hits.Length-1)
                {
                    while(counter < nukeCount)
                    {
                        startPos = transform.position + new Vector3(0, 10f, 0) + Random.insideUnitSphere * 2f;
                        nuke = nukePool.Get().GetComponent<Rocket>();
                        nuke.transform.position = startPos;
                        nuke.SetRocketActive(hits[i].transform.position);
                        counter++;
                    }
                }
            }
            return true;
        }

    }
}