using Core.EventBus;
using Data.ScriptableObjects;

namespace Modules.Bonus.Events
{
    public struct QuickCollectBonusEvent : IEvent
    {
        public float Multiplier;
        public float Duration;
        public DropItemType BonusType;

        public QuickCollectBonusEvent(float multiplier, float duration, DropItemType bonusType)
        {
            Multiplier = multiplier;
            Duration = duration;
            BonusType = bonusType;
        }
    }
}