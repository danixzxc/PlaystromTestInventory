using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Modules.Chest.View
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _spawnDuration = 0.8f;
        [SerializeField] private float _spawnStartY = 5f;
        [SerializeField] private Sprite _closedSprite;

        public UnityEvent OnChestClicked = new UnityEvent();
        public UnityEvent OnSpawnComplete = new UnityEvent();
        public UnityEvent OnChestOpenAnimationPeak = new UnityEvent();

        private Tween _currentTween;
        private bool _interactable;

        private void Awake()
        {
            if (_closedSprite != null)
            {
                _spriteRenderer.sprite = _closedSprite;
            }
        }

        public void PlaySpawnAnimation(Vector3 targetPosition)
        {
            _interactable = false;
            _spriteRenderer.color = Color.white;

            _animator.Rebind();

            transform.position = new Vector3(targetPosition.x, _spawnStartY, targetPosition.z);

            _currentTween?.Kill();
            _currentTween = transform.DOMoveY(targetPosition.y, _spawnDuration)
                .SetEase(Ease.OutBounce)
                .OnComplete(() =>
                {
                    OnSpawnComplete.Invoke();
                });
        }

        public void ShowIdleState()
        {
            _interactable = true;
            _animator.SetTrigger("Idle");
        }

        public void PlayOpenAnimation()
        {
            _interactable = false;
            _animator.SetTrigger("Open");
            transform.DOPunchScale(Vector3.one * 0.2f, 0.3f);
        }

        public void PlayDisappearAnimation()
        {
            _interactable = false;

            _currentTween?.Kill();
            _currentTween = _spriteRenderer.DOFade(0f, 0.5f)
                .SetEase(Ease.InQuad);
        }

        private void OnMouseDown()
        {
            if (_interactable)
            {
                OnChestClicked.Invoke();
            }
        }

        public void OnChestOpenedAnimationEvent()
        {
            OnChestOpenAnimationPeak.Invoke();
        }

        private void OnDestroy()
        {
            _currentTween?.Kill();
        }
    }
}