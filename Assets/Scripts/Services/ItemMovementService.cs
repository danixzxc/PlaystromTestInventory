using System;
using System.Collections.Generic;
using Data.ScriptableObjects;
using Modules.Drop.View;
using Modules.Inventory.Model;
using Modules.Bonus.Model;
using UnityEngine;
using Modules.Drop.Commands;
using Zenject;

namespace Services
{
    public class ItemMovementService
    {
        private readonly AnimationService _animationService;
        private readonly InventoryModel _inventoryModel;
        private readonly BonusModel _bonusModel;
        private readonly BonusService _bonusService;
        private readonly Dictionary<DropItemType, RectTransform> _targets;

        public ItemMovementService(
            AnimationService animationService,
            InventoryModel inventoryModel,
            BonusModel bonusModel,
            BonusService bonusService,
            [Inject(Id = "CoinTarget")] RectTransform coinTarget,
            [Inject(Id = "CrystalTarget")] RectTransform crystalTarget,
            [Inject(Id = "HealthTarget")] RectTransform healthTarget)
        {
            _animationService = animationService;
            _inventoryModel = inventoryModel;
            _bonusModel = bonusModel;
            _bonusService = bonusService;

            _targets = new Dictionary<DropItemType, RectTransform>
            {
                { DropItemType.Coin, coinTarget },
                { DropItemType.Crystal, crystalTarget },
                { DropItemType.HealthPotion, healthTarget }
            };
        }

        public void CollectItem(BaseDropItemView dropView, DropItemConfig config, Action onComplete = null)
        {
            RectTransform target = _targets[config.ItemType];

            if (_bonusModel.IsBonusActive && config.ItemType != _bonusModel.BonusType)
            {
                _bonusService.DeactivateBonus();
            }
            else
            {
                _bonusService.RegisterCollection(config.ItemType);
            }

            _animationService.FlyToTarget(dropView.transform, Camera.main.ScreenToWorldPoint(target.position), () =>
            {
                ApplyCollection(config);
                dropView.ReturnToPool();
                onComplete?.Invoke();
            });
        }

        public void DropToGround(BaseDropItemView dropView, Vector3 groundPosition, Action onComplete = null)
        {
            _animationService.FlyToTarget(dropView.transform, groundPosition, () =>
            {
                onComplete?.Invoke();
            }, false);
        }

        private void ApplyCollection(DropItemConfig config)
        {
            CollectItemCommand command = new CollectItemCommand(_inventoryModel, _bonusModel);
            command.Execute(config);
        }
    }
}