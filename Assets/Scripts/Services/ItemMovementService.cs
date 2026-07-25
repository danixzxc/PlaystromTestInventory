using System;
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
        private readonly RectTransform _coinTarget;
        private readonly RectTransform _crystalTarget;
        private readonly RectTransform _healthTarget;

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
            _coinTarget = coinTarget;
            _crystalTarget = crystalTarget;
            _healthTarget = healthTarget;
        }

        public void CollectItem(BaseDropItemView dropView, DropItemConfig config, Action onComplete = null)
        {
            RectTransform target = GetTargetForType(config.ItemType);

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

        private RectTransform GetTargetForType(DropItemType type)
        {
            switch (type)
            {
                case DropItemType.Coin:
                    return _coinTarget;
                case DropItemType.Crystal:
                    return _crystalTarget;
                case DropItemType.HealthPotion:
                    return _healthTarget;
                default:
                    return _coinTarget;
            }
        }

        private void ApplyCollection(DropItemConfig config)
        {
            CollectItemCommand command = new CollectItemCommand(_inventoryModel, _bonusModel);
            command.Execute(config);
        }
    }
}