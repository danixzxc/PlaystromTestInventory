using System;
using System.Collections.Generic;
using Data.ScriptableObjects;
using Modules.Bonus.Model;
using Modules.Inventory.Model;
using UnityEngine;

namespace Modules.Drop.Commands
{
    public class CollectItemCommand
    {
        private readonly InventoryModel _inventoryModel;
        private readonly BonusModel _bonusModel;
        private readonly Dictionary<DropItemType, Action<int>> _collectActions;

        public CollectItemCommand(InventoryModel inventoryModel, BonusModel bonusModel)
        {
            _inventoryModel = inventoryModel;
            _bonusModel = bonusModel;

            _collectActions = new Dictionary<DropItemType, Action<int>>
            {
                { DropItemType.Coin, value => _inventoryModel.AddCoins(value) },
                { DropItemType.Crystal, value => _inventoryModel.AddCrystals(value) },
                { DropItemType.HealthPotion, value => _inventoryModel.RestoreHealth(value) }
            };
        }

        public void Execute(DropItemConfig config)
        {
            int multipliedValue = Mathf.RoundToInt(config.Value * _bonusModel.CurrentMultiplier);
            _collectActions[config.ItemType]?.Invoke(multipliedValue);
        }
    }
}