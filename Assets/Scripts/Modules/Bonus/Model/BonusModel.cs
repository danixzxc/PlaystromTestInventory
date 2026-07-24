namespace Modules.Bonus.Model
{
    public class BonusModel
    {
        public float CurrentMultiplier { get; private set; } = 1f;
        public bool IsBonusActive { get; private set; }

        public void ActivateBonus(float multiplier)
        {
            CurrentMultiplier = multiplier;
            IsBonusActive = true;
        }

        public void DeactivateBonus()
        {
            CurrentMultiplier = 1f;
            IsBonusActive = false;
        }
    }
}