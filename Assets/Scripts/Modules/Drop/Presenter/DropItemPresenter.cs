using System;
using Data.ScriptableObjects;
using Modules.Drop.View;
using Services;

namespace Modules.Drop.Presenter
{
    public class DropItemPresenter
    {
        private readonly BaseDropItemView _view;
        private readonly DropItemConfig _config;
        private readonly ItemMovementService _itemMovementService;

        public event Action OnCollected;

        public DropItemPresenter(
            BaseDropItemView view,
            DropItemConfig config,
            ItemMovementService itemMovementService)
        {
            _view = view;
            _config = config;
            _itemMovementService = itemMovementService;
            _view.OnItemClicked.AddListener(OnItemClicked);
        }

        private void OnItemClicked()
        {
            _view.OnItemClicked.RemoveListener(OnItemClicked);
            _itemMovementService.CollectItem(_view, _config, () => OnCollected?.Invoke());
        }

        public void Dispose()
        {
            _view.OnItemClicked.RemoveListener(OnItemClicked);
        }
    }
}