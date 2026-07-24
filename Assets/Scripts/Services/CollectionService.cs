// Assets/_Project/Scripts/Services/CollectionService.cs
using Data.ScriptableObjects;
using Modules.Drop.View;
using Modules.Inventory.Model;
using UnityEngine;
using Modules.Drop.Commands;
using Core.Pool;

namespace Services
{
    public class CollectionService
    {
        private readonly AnimationService _animationService;
        private readonly InventoryModel _inventoryModel;
        private readonly BonusService _bonusService;
        private readonly RectTransform _coinTarget;
        private readonly RectTransform _crystalTarget;
        private readonly RectTransform _healthTarget;

        public CollectionService(
            AnimationService animationService,
            InventoryModel inventoryModel,
            BonusService bonusService,
            RectTransform coinTarget,
            RectTransform crystalTarget,
            RectTransform healthTarget)
        {
            _animationService = animationService;
            _inventoryModel = inventoryModel;
            _bonusService = bonusService;
            _coinTarget = coinTarget;
            _crystalTarget = crystalTarget;
            _healthTarget = healthTarget;
        }

        public void CollectItem(BaseDropItemView dropView, DropItemConfig config)
        {
            RectTransform target = GetTargetForType(config.ItemType);
            _bonusService.RegisterCollection(config.ItemType);

            _animationService.FlyToTarget(dropView.transform, target, () =>
            {
                ApplyCollection(config);
                dropView.ReturnToPool();
            });
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
            CollectItemCommand command = new CollectItemCommand(_inventoryModel);
            command.Execute(config);
        }
    }
}