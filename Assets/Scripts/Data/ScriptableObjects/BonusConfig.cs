using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BonusConfig", menuName = "Inventory/Bonus Config")]
    public class BonusConfig : ScriptableObject
    {
        public int RequiredItemsForBonus = 3;
        public float TimeWindowSeconds = 2f;
        public float BonusDurationSeconds = 5f;
        public float BonusMultiplier = 1.5f;
    }
}