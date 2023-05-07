using UnityEngine;

namespace Interfaces
{
    public interface IDamageable // OBJECT THAT CAN TAKE DAMAGE
    {
        public void ApplyDamage(RaycastHit hit, float baseDamage); // Hit Infos and Damage
    }
}