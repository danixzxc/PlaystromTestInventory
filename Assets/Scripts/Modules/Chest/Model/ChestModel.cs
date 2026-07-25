using Data.ScriptableObjects;
using System.Collections.Generic;

namespace Modules.Chest.Model
{
    public class ChestModel
    {
        private readonly ChestConfig _chestConfig;

        public ChestModel(ChestConfig chestConfig)
        {
            _chestConfig = chestConfig;
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