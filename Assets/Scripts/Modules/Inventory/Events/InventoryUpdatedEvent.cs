using Core.EventBus;

namespace Modules.Inventory.Events
{
    public struct InventoryUpdatedEvent : IEvent
    {
        public int Coins;
        public int Crystals;
        public int CurrentHealth;
        public int MaxHealth;
    }
}