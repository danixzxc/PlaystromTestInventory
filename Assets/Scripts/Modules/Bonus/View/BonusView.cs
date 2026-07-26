using System.Collections.Generic;
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
        [SerializeField] private float _scaleInDuration = 0.3f;
        [SerializeField] private float _scaleOutDuration = 0.3f;
        [SerializeField] private float _scaleMultiplier = 1.2f;
        [SerializeField] private BonusTypeColor[] _bonusTypeColors;

        public RectTransform RectTransform { get; private set; }

        private Dictionary<DropItemType, Color> _colorMap;
        private Tweener _currentTweener;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            _bonusIndicator.SetActive(false);
            _bonusIndicator.transform.localScale = Vector3.zero;

            _colorMap = new Dictionary<DropItemType, Color>();
            foreach (var bonusTypeColor in _bonusTypeColors)
            {
                _colorMap[bonusTypeColor.Type] = bonusTypeColor.Color;
            }
        }

        public void ShowBonus(float duration, DropItemType bonusType)
        {
            _bonusIndicator.SetActive(true);
            _bonusTimerSlider.value = 1f;
            _sliderFillImage.color = _colorMap.ContainsKey(bonusType) ? _colorMap[bonusType] : Color.white;

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

    }
}