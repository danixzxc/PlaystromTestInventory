using Data.ScriptableObjects;
using Modules.Drop.View;
using UnityEngine;
using Core.Pool;

namespace Modules.Drop.Factory
{
    public class DropItemFactory
    {
        private readonly ObjectPool _coinPool;
        private readonly ObjectPool _crystalPool;
        private readonly ObjectPool _healthPotionPool;

        public DropItemFactory(
            ObjectPool coinPool,
            ObjectPool crystalPool,
            ObjectPool healthPotionPool)
        {
            _coinPool = coinPool;
            _crystalPool = crystalPool;
            _healthPotionPool = healthPotionPool;
        }

        public BaseDropItemView CreateDrop(DropItemConfig config, Vector3 position)
        {
            BaseDropItemView dropView = GetDropFromPool(config.ItemType);
            dropView.transform.position = position;
            dropView.SetSprite(config.Icon);
            return dropView;
        }

        private BaseDropItemView GetDropFromPool(DropItemType type)
        {
            switch (type)
            {
                case DropItemType.Coin:
                    return _coinPool.Get<BaseDropItemView>();
                case DropItemType.Crystal:
                    return _crystalPool.Get<BaseDropItemView>();
                case DropItemType.HealthPotion:
                    return _healthPotionPool.Get<BaseDropItemView>();
                default:
                    return null;
            }
        }
    }
}