using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Inventory.View
{
    public class CrystalIndicatorView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private float _tweenDuration = 0.3f;
        [SerializeField] private float _scaleMultiplier = 1.3f;

        private Tweener _currentTweener;

        public void Initialize(int amount)
        {
            _amountText.text = amount.ToString();
            _iconImage.transform.localScale = Vector3.one;
        }

        public void UpdateAmount(int amount)
        {
            if (_amountText.text == amount.ToString())
            {
                return;
            }
            _amountText.text = amount.ToString();

            _currentTweener?.Kill();

            _iconImage.transform.localScale = Vector3.one;

            _currentTweener = _iconImage.transform
                .DOScale(Vector3.one * _scaleMultiplier, _tweenDuration * 0.5f)
                .OnComplete(() =>
                {
                    _currentTweener = _iconImage.transform
                        .DOScale(Vector3.one, _tweenDuration * 0.5f);
                });
        }
    }
}