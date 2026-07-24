using Core.EventBus;

namespace Modules.Bonus.Events
{
    public struct QuickCollectBonusEvent : IEvent
    {
        public float Multiplier;
        public QuickCollectBonusEvent(float multiplier) { Multiplier = multiplier; }
    }
}