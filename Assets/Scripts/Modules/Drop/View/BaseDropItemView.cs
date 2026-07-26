using Core.Pool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Modules.Drop.View
{
    public class BaseDropItemView : PooledItem, IPointerClickHandler
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected Vector3 _defaultScale = new Vector3(5, 5, 1);

        public UnityEvent OnItemClicked = new UnityEvent();
        public RectTransform RectTransform { get; private set; }

        protected virtual void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnItemClicked.Invoke();
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void SetSortingOrder(int order)
        {
            _spriteRenderer.sortingOrder = order;
        }

        public override void OnSpawn()
        {
            gameObject.transform.localScale = _defaultScale;
        }
    }
}