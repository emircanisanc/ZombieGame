using UnityEngine;

namespace Interfaces
{
    public interface ISlowable // OBJECT THAT CAN SLOW
    {
        public void ApplySlow(float slowAmount, float slowDuration);
    }
}