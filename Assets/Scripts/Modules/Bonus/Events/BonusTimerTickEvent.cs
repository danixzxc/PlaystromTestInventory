using Core.EventBus;

namespace Modules.Bonus.Events
{
    public struct BonusTimerTickEvent : IEvent
    {
        public float DeltaTime;

        public BonusTimerTickEvent(float deltaTime)
        {
            DeltaTime = deltaTime;
        }
    }
}