using Data.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Chest.Model
{
    public enum ChestState
    {
        Spawning,
        Idle,
        Opened,
        Collecting,
        Disappearing,
        Inactive
    }

    public class ChestModel
    {
        private readonly ChestConfig _chestConfig;

        public ChestState CurrentState { get; private set; } = ChestState.Inactive;
        public int TotalDropsSpawned { get; private set; }
        public int DropsCollected { get; private set; }


        public ChestModel(ChestConfig chestConfig)
        {
            _chestConfig = chestConfig;
        }

        public void SetState(ChestState state)
        {
            CurrentState = state;
        }

        public void SetDropsCount(int count)
        {
            TotalDropsSpawned = count;
            DropsCollected = 0;
        }

        public void RegisterDropCollected()
        {
            DropsCollected++;
        }

        public bool AllDropsCollected()
        {
            return TotalDropsSpawned > 0 && DropsCollected >= TotalDropsSpawned;
        }

        public List<DropItemConfig> GetRandomDrops()
        {
            List<DropItemConfig> drops = new List<DropItemConfig>();
            int dropCount = UnityEngine.Random.Range(_chestConfig.MinDrops, _chestConfig.MaxDrops + 1);

            DropItemConfig selectedDrop = _chestConfig.GetRandomDrop();
            for (int i = 0; i < dropCount; i++)
            {
                drops.Add(selectedDrop);
            }

            return drops;
        }
    }
}