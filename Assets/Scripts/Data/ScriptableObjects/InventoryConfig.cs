using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "InventoryConfig", menuName = "Inventory/Inventory Config")]
    public class InventoryConfig : ScriptableObject
    {
        public int InitialCoins = 0;
        public int InitialCrystals = 0;
        public int MaxHealth = 100;
        public int InitialHealth = 100;
    }
}