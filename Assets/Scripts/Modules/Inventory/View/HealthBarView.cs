using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Inventory.View
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Slider _instantHealthSlider;
        [SerializeField] private Slider _delayedHealthSlider;
        [SerializeField] private float _delayDuration = 0.2f;
        [SerializeField] private float _tweenDuration = 0.4f;

        private Tweener _currentTweener;

        public void InitializeHealth(float current, float max)
        {
            float targetValue = current / max;

            _instantHealthSlider.value = targetValue;
            _delayedHealthSlider.value = targetValue;
        }
        public void UpdateHealth(float current, float max)
        {
            float targetValue = current / max;

            _instantHealthSlider.value = targetValue;

            _currentTweener?.Kill();
            _currentTweener = DOTween.To(
                () => _delayedHealthSlider.value,
                x => _delayedHealthSlider.value = x,
                targetValue,
                _tweenDuration
            ).SetDelay(_delayDuration);
        }
    }
}