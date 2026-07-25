using Modules.Inventory.Events;
using Modules.Inventory.View;
using Core.EventBus;

namespace Modules.Inventory.Presenter
{
    public class CoinPresenter
    {
        private readonly CoinIndicatorView _view;

        public CoinPresenter(CoinIndicatorView view)
        {
            _view = view;
            EventBus.Subscribe<InventoryInitializedEvent>(OnInventoryInitialized);
            EventBus.Subscribe<InventoryUpdatedEvent>(OnInventoryUpdated);
        }

        private void OnInventoryInitialized(InventoryInitializedEvent eventData)
        {
            _view.Initialize(eventData.Coins);
        }
        private void OnInventoryUpdated(InventoryUpdatedEvent eventData)
        {
            _view.UpdateAmount(eventData.Coins);
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<InventoryInitializedEvent>(OnInventoryInitialized);
            EventBus.Unsubscribe<InventoryUpdatedEvent>(OnInventoryUpdated);
        }
    }
}