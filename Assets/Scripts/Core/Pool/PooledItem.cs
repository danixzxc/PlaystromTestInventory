using UnityEngine;

namespace Core.Pool
{
    public class PooledItem : MonoBehaviour
    {
        public ObjectPool Pool { get; set; }

        public virtual void OnSpawn() { }
        public virtual void OnDespawn() { }

        public void ReturnToPool()
        {
            Pool?.Return(this);
        }
    }
}