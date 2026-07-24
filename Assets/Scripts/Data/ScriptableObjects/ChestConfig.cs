using System;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [Serializable]
    public struct DropChance
    {
        public DropItemConfig ItemConfig;
        [Range(0f, 1f)]
        public float Weight;
    }

    [CreateAssetMenu(fileName = "ChestConfig", menuName = "Inventory/Chest Config")]
    public class ChestConfig : ScriptableObject
    {
        public DropChance[] DropTable;
        public int MinDrops = 1;
        public int MaxDrops = 3;

        public DropItemConfig GetRandomDrop()
        {
            float totalWeight = 0f;
            foreach (var drop in DropTable)
            {
                totalWeight += drop.Weight;
            }

            float randomValue = UnityEngine.Random.Range(0f, totalWeight);
            float cumulativeWeight = 0f;

            foreach (var drop in DropTable)
            {
                cumulativeWeight += drop.Weight;
                if (randomValue <= cumulativeWeight)
                {
                    return drop.ItemConfig;
                }
            }

            return DropTable.Length > 0 ? DropTable[0].ItemConfig : null;
        }
    }
}