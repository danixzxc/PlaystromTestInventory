using Core.Pool;
using Data.ScriptableObjects;
using Modules.Drop.View;
using UnityEngine;
using Zenject;
using Modules.Drop.Presenter;
using Services;
using System.Collections.Generic;

namespace Modules.Drop.Factory
{
    public class DropItemFactory
    {
        private readonly ObjectPool _coinPool;
        private readonly ObjectPool _crystalPool;
        private readonly ObjectPool _healthPotionPool;
        private readonly CollectionService _collectionService;
        private readonly List<DropItemPresenter> _activePresenters = new List<DropItemPresenter>();

        public DropItemFactory(
            [Inject(Id = "CoinPool")] ObjectPool coinPool,
            [Inject(Id = "CrystalPool")] ObjectPool crystalPool,
            [Inject(Id = "HealthPool")] ObjectPool healthPotionPool,
            CollectionService collectionService)
        {
            _coinPool = coinPool;
            _crystalPool = crystalPool;
            _healthPotionPool = healthPotionPool;
            _collectionService = collectionService;
        }

        public BaseDropItemView CreateDrop(DropItemConfig config, Vector3 position)
        {
            BaseDropItemView dropView = GetDropFromPool(config.ItemType);
            dropView.transform.position = position;
            dropView.SetSprite(config.Icon);

            DropItemPresenter presenter = new DropItemPresenter(dropView, config, _collectionService);
            _activePresenters.Add(presenter);

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