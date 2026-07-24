using Core.EventBus;

namespace Modules.Inventory.Events
{
    public struct CoinCollectedEvent : IEvent
    {
        public int Amount;
        public CoinCollectedEvent(int amount) { Amount = amount; }
    }
}