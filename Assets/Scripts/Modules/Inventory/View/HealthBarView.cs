using UnityEngine;
using UnityEngine.UI;

namespace Modules.Inventory.View
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        public RectTransform RectTransform { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void UpdateHealth(float current, float max)
        {
            _healthSlider.value = current / max;
        }
    }
}