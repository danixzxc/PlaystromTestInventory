using System.Collections.Generic;
using UnityEngine;

namespace Core.Pool
{
    public class ObjectPool
    {
        private readonly PooledItem _prefab;
        private readonly Transform _parent;
        private readonly Queue<PooledItem> _pool = new Queue<PooledItem>();

        public ObjectPool(PooledItem prefab, int initialSize, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            for (int i = 0; i < initialSize; i++)
            {
                CreateNew();
            }
        }

        private PooledItem CreateNew()
        {
            PooledItem item = Object.Instantiate(_prefab, _parent);
            item.Pool = this;
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
            return item;
        }

        public T Get<T>() where T : PooledItem
        {
            if (_pool.Count == 0)
            {
                CreateNew();
            }

            PooledItem item = _pool.Dequeue();
            item.gameObject.SetActive(true);
            item.OnSpawn();
            return item as T;
        }

        public void Return(PooledItem item)
        {
            item.OnDespawn();
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }
    }
}