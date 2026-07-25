using Core.Pool;
using Data.ScriptableObjects;
using Modules.Drop.View;
using Modules.Drop.Presenter;
using Services;
using UnityEngine;
using Zenject;
using System;

namespace Modules.Drop.Factory
{
    public class DropItemFactory
    {
        private readonly ObjectPool _coinPool;
        private readonly ObjectPool _crystalPool;
        private readonly ObjectPool _healthPotionPool;
        private readonly ItemMovementService _itemMovementService;

        public DropItemFactory(
            [Inject(Id = "CoinPool")] ObjectPool coinPool,
            [Inject(Id = "CrystalPool")] ObjectPool crystalPool,
            [Inject(Id = "HealthPool")] ObjectPool healthPotionPool,
            ItemMovementService itemMovementService)
        {
            _coinPool = coinPool;
            _crystalPool = crystalPool;
            _healthPotionPool = healthPotionPool;
            _itemMovementService = itemMovementService;
        }

        public BaseDropItemView CreateDrop(DropItemConfig config, Vector3 spawnPosition, Action onCollected = null)
        {
            BaseDropItemView dropView = GetDropFromPool(config.ItemType);
            dropView.transform.position = spawnPosition;
            dropView.SetSprite(config.Icon);

            Vector3 groundPosition = spawnPosition + new Vector3(
                UnityEngine.Random.Range(-3f, 3f),
                -2f,
                0f
            );

            _itemMovementService.DropToGround(dropView, groundPosition, () =>
            {
                DropItemPresenter presenter = new DropItemPresenter(dropView, config, _itemMovementService);
                if (onCollected != null)
                {
                    presenter.OnCollected += onCollected;
                }
            });

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