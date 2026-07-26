using System;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ChestConfig", menuName = "Inventory/Chest Config")]
    public class ChestConfig : ScriptableObject
    {
        public DropChance[] DropTable;
        public int MinDrops = 1;
        public int MaxDrops = 3;
        public float RespawnDelay = 2f;
    }
}