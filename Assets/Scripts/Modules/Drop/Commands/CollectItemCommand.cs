using Core.EventBus;
using Data.ScriptableObjects;
using Modules.Bonus.Model;
using Modules.Drop.View;
using Modules.Inventory.Events;
using Modules.Inventory.Model;
using UnityEngine;

namespace Modules.Drop.Commands
{
    public class CollectItemCommand
    {
        private readonly InventoryModel _inventoryModel;
        private readonly BonusModel _bonusModel;

        public CollectItemCommand(InventoryModel inventoryModel, BonusModel bonusModel)
        {
            _inventoryModel = inventoryModel;
            _bonusModel = bonusModel;
        }

        public void Execute(DropItemConfig config)
        {
            int multipliedValue = Mathf.RoundToInt(config.Value * _bonusModel.CurrentMultiplier);

            switch (config.ItemType)
            {
                case DropItemType.Coin:
                    _inventoryModel.AddCoins(multipliedValue);
                    break;
                case DropItemType.Crystal:
                    _inventoryModel.AddCrystals(multipliedValue);
                    break;
                case DropItemType.HealthPotion:
                    _inventoryModel.RestoreHealth(multipliedValue);
                    break;
            }
        }
    }
}