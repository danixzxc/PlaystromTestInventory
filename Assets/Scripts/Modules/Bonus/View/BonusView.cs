using Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Bonus.View
{
    public class BonusView : MonoBehaviour
    {
        [SerializeField] private GameObject _bonusIndicator;
        [SerializeField] private Slider _bonusTimerSlider;
        [SerializeField] private Image _sliderFillImage;
        [SerializeField] private Color _coinColor = Color.yellow;
        [SerializeField] private Color _healthColor = Color.green;
        [SerializeField] private Color _crystalColor = Color.cyan;

        public RectTransform RectTransform { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            _bonusIndicator.SetActive(false);
        }

        public void ShowBonus(float duration, DropItemType bonusType)
        {
            _bonusIndicator.SetActive(true);
            _bonusTimerSlider.value = 1f;
            _sliderFillImage.color = GetColorByType(bonusType);
        }

        public void UpdateTimer(float progress)
        {
            _bonusTimerSlider.value = progress;
        }

        public void HideBonus()
        {
            _bonusIndicator.SetActive(false);
        }

        private Color GetColorByType(DropItemType type)
        {
            switch (type)
            {
                case DropItemType.Coin:
                    return _coinColor;
                case DropItemType.HealthPotion:
                    return _healthColor;
                case DropItemType.Crystal:
                    return _crystalColor;
                default:
                    return Color.white;
            }
        }
    }
}