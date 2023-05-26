using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

namespace Upgrade
{
    public class SlashUpgrade : UpgradeBase
    {
        [SerializeField] private SlashUpgradeData slashUpgradeData;
        [SerializeField] private GameObject slashParticle;
        [SerializeField] private float slashParticleDuration;
        [SerializeField] private LayerMask enemyLayer;

        public override UpgradeDataBase UpgradeData => slashUpgradeData;

        [SerializeField] private float slashRadius;
        private float slashDamage;
        private float slashDelay;
        private float nextSlashTime;
        private bool isEnable = false;

        RaycastHit[] hits;

        void Awake()
        {
            hits = new RaycastHit[15];
        }

        protected override void OnLevelUp()
        {
            slashDamage = slashUpgradeData.slashDatas[level].slashDamage;
            slashDelay = slashUpgradeData.slashDatas[level].slashDelay;
            slashRadius = slashUpgradeData.slashDatas[level].slashRadius;
            var size = slashUpgradeData.slashDatas[level].slashParticleSize;
            slashParticle.transform.localScale = new Vector3(size, size, size);
            if (level > 0)
            {
                isEnable = true;
                nextSlashTime = Time.timeSinceLevelLoad + slashDelay;
            }
        }

        void FixedUpdate()
        {
            if (!isEnable)
                return;
            if (Time.timeSinceLevelLoad >= nextSlashTime)
            {
                nextSlashTime = Time.timeSinceLevelLoad + slashDelay;
                Slash();
            }
        }
        private void Slash()
        {
            slashParticle.SetActive(true);
            Invoke("SetDisableParticle", slashParticleDuration);
            ApplyDamageToArea();
        }
        private void ApplyDamageToArea()
        {
            Physics.SphereCastNonAlloc(transform.position, slashRadius, transform.forward, hits, 0f, enemyLayer);
            foreach (RaycastHit hit in hits)
            {
                if (!hit.collider)
                    continue;
                if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.ApplyDamage(hit, slashDamage);
                }
            }
        }

        private void SetDisableParticle()
        {
            slashParticle.SetActive(false);
        }

    }
}