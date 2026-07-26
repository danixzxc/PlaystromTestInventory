using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Inventory.View
{
    public abstract class BaseResourceView : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _amountText;
        [SerializeField] protected Image _iconImage;
        [SerializeField] protected float _tweenDuration = 0.3f;
        [SerializeField] protected float _scaleMultiplier = 1.3f;
        [SerializeField] protected float _scaleInDurationMultiplier = 0.5f;
        [SerializeField] protected float _scaleOutDurationMultiplier = 0.5f;

        protected Tweener _currentTweener;

        public virtual void Initialize(int amount)
        {
            _amountText.text = amount.ToString();
            _iconImage.transform.localScale = Vector3.one;
        }

        public virtual void UpdateAmount(int amount)
        {
            if (_amountText.text == amount.ToString()) return;

            _amountText.text = amount.ToString();
            PlayUpdateAnimation();
        }

        protected virtual void PlayUpdateAnimation()
        {
            _currentTweener?.Kill();
            _iconImage.transform.localScale = Vector3.one;

            float scaleInDuration = _tweenDuration * _scaleInDurationMultiplier;
            float scaleOutDuration = _tweenDuration * _scaleOutDurationMultiplier;

            _currentTweener = _iconImage.transform
                .DOScale(Vector3.one * _scaleMultiplier, scaleInDuration)
                .OnComplete(() =>
                {
                    _currentTweener = _iconImage.transform
                        .DOScale(Vector3.one, scaleOutDuration);
                });
        }
    }
}