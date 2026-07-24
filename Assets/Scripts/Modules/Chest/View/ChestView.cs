using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Modules.Chest.View
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private Sprite _closedSprite;
        [SerializeField] private Sprite _openSprite;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public UnityEvent OnChestClicked = new UnityEvent();

        private bool _isOpened = false;

        private void Awake()
        {
            _spriteRenderer.sprite = _closedSprite;
        }

        private void OnMouseDown()
        {
            if (!_isOpened)
            {
                OnChestClicked.Invoke();
            }
        }

        public void Open()
        {
            _isOpened = true;
            _spriteRenderer.sprite = _openSprite;
            transform.DOPunchScale(Vector3.one * 0.2f, 0.3f);
        }
    }
}