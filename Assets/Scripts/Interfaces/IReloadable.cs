using System;

namespace Interfaces
{
    public interface IReloadable
    {
        public Action OnReload { get; set; }
        public Action OnReloadEnd { get; set; }
    }
}