using Core.EventBus;

namespace Modules.Inventory.Events
{
    public struct CrystalCollectedEvent : IEvent
    {
        public int Amount;
        public CrystalCollectedEvent(int amount) { Amount = amount; }
    }
}