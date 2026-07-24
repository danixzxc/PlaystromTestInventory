using Data.ScriptableObjects;
using Modules.Drop.View;
using Modules.Inventory.Events;
using Modules.Inventory.Model;
using Core.EventBus;

namespace Modules.Drop.Commands
{
    public class CollectItemCommand
    {
        private readonly InventoryModel _inventoryModel;

        public CollectItemCommand(InventoryModel inventoryModel)
        {
            _inventoryModel = inventoryModel;
        }

        public void Execute(DropItemConfig config)
        {
            switch (config.ItemType)
            {
                case DropItemType.Coin:
                    _inventoryModel.AddCoins(config.Value);
                    break;
                case DropItemType.Crystal:
                    _inventoryModel.AddCrystals(config.Value);
                    break;
                case DropItemType.HealthPotion:
                    _inventoryModel.RestoreHealth(config.Value);
                    break;
            }
        }
    }
}