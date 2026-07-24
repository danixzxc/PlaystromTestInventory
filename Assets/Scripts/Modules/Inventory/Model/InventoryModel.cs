using Data.ScriptableObjects;
using Modules.Inventory.Events;
using Core.EventBus;

namespace Modules.Inventory.Model
{
    public class InventoryModel
    {
        public int Coins { get; private set; }
        public int Crystals { get; private set; }
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }

        public InventoryModel(InventoryConfig config)
        {
            Coins = config.InitialCoins;
            Crystals = config.InitialCrystals;
            MaxHealth = config.MaxHealth;
            CurrentHealth = config.InitialHealth;
        }

        public void AddCoins(int amount)
        {
            Coins += amount;
            EventBus.Fire(new CoinCollectedEvent(amount));
            FireUpdatedEvent();
        }

        public void AddCrystals(int amount)
        {
            Crystals += amount;
            EventBus.Fire(new CrystalCollectedEvent(amount));
            FireUpdatedEvent();
        }

        public void RestoreHealth(int amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            EventBus.Fire(new HealthRestoredEvent(amount));
            FireUpdatedEvent();
        }

        private void FireUpdatedEvent()
        {
            EventBus.Fire(new InventoryUpdatedEvent
            {
                Coins = Coins,
                Crystals = Crystals,
                CurrentHealth = CurrentHealth,
                MaxHealth = MaxHealth
            });
        }
    }
}