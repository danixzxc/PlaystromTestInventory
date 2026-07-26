using Modules.Inventory.Events;
using Modules.Inventory.View;
using Core.EventBus;

namespace Modules.Inventory.Presenter
{
    public abstract class BaseResourcePresenter<TView> where TView : BaseResourceView
    {
        protected readonly TView _view;

        protected BaseResourcePresenter(TView view)
        {
            _view = view;
            EventBus.Subscribe<InventoryInitializedEvent>(OnInventoryInitialized);
            EventBus.Subscribe<InventoryUpdatedEvent>(OnInventoryUpdated);
        }

        protected abstract void OnInventoryInitialized(InventoryInitializedEvent eventData);
        protected abstract void OnInventoryUpdated(InventoryUpdatedEvent eventData);

        public void Dispose()
        {
            EventBus.Unsubscribe<InventoryInitializedEvent>(OnInventoryInitialized);
            EventBus.Unsubscribe<InventoryUpdatedEvent>(OnInventoryUpdated);
        }
    }
}