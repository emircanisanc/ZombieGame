using System;

namespace Interfaces
{
    public interface IFireable
    {
        public Action OnStartFire { get; set; }
        public Action OnFire { get; set; }
        public Action OnStopFire { get; set; }
    }
}