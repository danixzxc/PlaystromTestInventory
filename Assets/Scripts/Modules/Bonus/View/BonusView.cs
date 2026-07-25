using Data.ScriptableObjects;
using DG.Tweening;
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
        [SerializeField] private float _scaleInDuration = 0.3f;
        [SerializeField] private float _scaleOutDuration = 0.3f;
        [SerializeField] private float _scaleMultiplier = 1.2f;

        public RectTransform RectTransform { get; private set; }

        private Tweener _currentTweener;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            _bonusIndicator.SetActive(false);
            _bonusIndicator.transform.localScale = Vector3.zero;
        }

        public void ShowBonus(float duration, DropItemType bonusType)
        {
            _bonusIndicator.SetActive(true);
            _bonusTimerSlider.value = 1f;
            _sliderFillImage.color = GetColorByType(bonusType);

            _currentTweener?.Kill();
            _bonusIndicator.transform.localScale = Vector3.zero;

            _currentTweener = _bonusIndicator.transform
                .DOScale(Vector3.one * _scaleMultiplier, _scaleInDuration)
                .SetEase(Ease.OutBack);
        }

        public void UpdateTimer(float progress)
        {
            _bonusTimerSlider.value = progress;
        }

        public void HideBonus()
        {
            _currentTweener?.Kill();

            _currentTweener = _bonusIndicator.transform
                .DOScale(Vector3.zero, _scaleOutDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    _bonusIndicator.SetActive(false);
                });
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