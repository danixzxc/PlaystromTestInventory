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
    }
}