using Data.ScriptableObjects;

namespace Modules.Bonus.Model
{
    public class BonusModel
    {
        public float CurrentMultiplier { get; private set; } = 1f;
        public bool IsBonusActive { get; private set; }
        public float RemainingTime { get; private set; }
        public float Duration { get; private set; }
        public DropItemType BonusType { get; private set; }

        public void ActivateBonus(float multiplier, float duration, DropItemType type)
        {
            CurrentMultiplier = multiplier;
            Duration = duration;
            RemainingTime = duration;
            BonusType = type;
            IsBonusActive = true;
        }

        public void UpdateRemainingTime(float deltaTime)
        {
            RemainingTime -= deltaTime;
        }

        public void DeactivateBonus()
        {
            CurrentMultiplier = 1f;
            IsBonusActive = false;
            RemainingTime = 0f;
        }

        public float GetProgress()
        {
            return Duration > 0 ? RemainingTime / Duration : 0f;
        }
    }
}