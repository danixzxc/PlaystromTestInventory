using UnityEngine;


namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Inventory/Audio Config")]
    public class AudioConfig : ScriptableObject
    {
        public SoundEntry[] Sounds;
    }
}