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
        private readonly CollectionService _collectionService;

        public event Action OnCollected;

        public DropItemPresenter(
            BaseDropItemView view,
            DropItemConfig config,
            CollectionService collectionService)
        {
            _view = view;
            _config = config;
            _collectionService = collectionService;
            _view.OnItemClicked.AddListener(OnItemClicked);
        }

        private void OnItemClicked()
        {
            _view.OnItemClicked.RemoveListener(OnItemClicked);
            _collectionService.CollectItem(_view, _config, () => OnCollected?.Invoke());
        }

        public void Dispose()
        {
            _view.OnItemClicked.RemoveListener(OnItemClicked);
        }
    }
}