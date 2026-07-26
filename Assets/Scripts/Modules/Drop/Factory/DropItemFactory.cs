using Core.Pool;
using Data.ScriptableObjects;
using Modules.Drop.View;
using Modules.Drop.Presenter;
using Services;
using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;

namespace Modules.Drop.Factory
{
    public class DropItemFactory
    {
        private readonly Dictionary<DropItemType, ObjectPool> _pools;
        private readonly ItemMovementService _itemMovementService;
        private int _sortingOrderCounter;

        public DropItemFactory(
            [Inject(Id = "CoinPool")] ObjectPool coinPool,
            [Inject(Id = "CrystalPool")] ObjectPool crystalPool,
            [Inject(Id = "HealthPool")] ObjectPool healthPotionPool,
            ItemMovementService itemMovementService)
        {
            _itemMovementService = itemMovementService;

            _pools = new Dictionary<DropItemType, ObjectPool>
            {
                { DropItemType.Coin, coinPool },
                { DropItemType.Crystal, crystalPool },
                { DropItemType.HealthPotion, healthPotionPool }
            };
        }

        public BaseDropItemView CreateDrop(DropItemConfig config, Vector3 spawnPosition, Action onCollected = null)
        {
            BaseDropItemView dropView = _pools[config.ItemType].Get<BaseDropItemView>();
            dropView.transform.position = spawnPosition;
            dropView.SetSprite(config.Icon);
            dropView.SetSortingOrder(++_sortingOrderCounter);

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
    }
}