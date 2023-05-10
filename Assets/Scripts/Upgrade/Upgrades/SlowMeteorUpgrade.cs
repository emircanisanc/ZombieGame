using UnityEngine;

namespace Upgrade
{
    [RequireComponent(typeof(MonoPool))]
    public class SlowMeteorUpgrade : UpgradeBase
    {
        [SerializeField] private float radius = 2f;
        [SerializeField] private SlowMeteorUpgradeData slowMeteorUpgradeData;
        public override UpgradeDataBase UpgradeData => slowMeteorUpgradeData;
        private MonoPool slowMeteorPool;

        private int meteorCount;
        private float spawnMeteorDelay;
        private float nextSpawnTime;
        private bool isEnable = false;

        [Header("SlowMeteor Datas")]
        [SerializeField] private Float damagePerHit;
        [SerializeField] private Float slowAmount;
        [SerializeField] private Float slowDuration;

        void Awake()
        {
            slowMeteorPool = GetComponent<MonoPool>();
        }

        protected override void OnLevelUp()
        {
            var data = slowMeteorUpgradeData.values[level];
            spawnMeteorDelay = data.spawnMeteorDelay;
            meteorCount = data.meteorCount;
            damagePerHit.Value = data.damage;
            slowAmount.Value = data.slowAmount;
            slowDuration.Value = data.slowDuration;
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

                var meteor = slowMeteorPool.Get().GetComponent<SlowMeteor>();
                meteor.SetMeteorActive(randomPos);

                angle += angleIncrement;
            }
        }

    }
}