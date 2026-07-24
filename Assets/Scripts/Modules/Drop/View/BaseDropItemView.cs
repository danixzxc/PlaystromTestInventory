using Core.Pool;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.Drop.View
{
    public class BaseDropItemView : PooledItem
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;

        public UnityEvent OnItemClicked = new UnityEvent();
        public RectTransform RectTransform { get; private set; }

        protected virtual void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        private void OnMouseDown()
        {
            OnItemClicked.Invoke();
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public override void OnSpawn()
        {
            gameObject.SetActive(true);
        }

        public override void OnDespawn()
        {
            gameObject.SetActive(false);
        }
    }
}