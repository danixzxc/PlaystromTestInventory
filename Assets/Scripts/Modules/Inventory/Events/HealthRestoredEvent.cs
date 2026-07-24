using Core.EventBus;

namespace Modules.Inventory.Events
{
    public struct HealthRestoredEvent : IEvent
    {
        public int Amount;
        public HealthRestoredEvent(int amount) { Amount = amount; }
    }
}