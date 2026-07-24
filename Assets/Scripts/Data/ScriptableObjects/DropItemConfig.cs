using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DropItemConfig", menuName = "Inventory/Drop Item Config")]
    public class DropItemConfig : ScriptableObject
    {
        public DropItemType ItemType;
        public Sprite Icon;
        public int Value;
        public GameObject Prefab;
    }
}