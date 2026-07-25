using Modules.Inventory.Events;
using Modules.Inventory.View;
using Core.EventBus;

namespace Modules.Inventory.Presenter
{
    public class CrystalPresenter
    {
        private readonly CrystalIndicatorView _view;

        public CrystalPresenter(CrystalIndicatorView view)
        {
            _view = view;
            EventBus.Subscribe<InventoryInitializedEvent>(OnInventoryInitialized);
            EventBus.Subscribe<InventoryUpdatedEvent>(OnInventoryUpdated);
        }

        private void OnInventoryInitialized(InventoryInitializedEvent eventData)
        {
            _view.Initialize(eventData.Crystals);
        }
        private void OnInventoryUpdated(InventoryUpdatedEvent eventData)
        {
            _view.UpdateAmount(eventData.Crystals);
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<InventoryInitializedEvent>(OnInventoryInitialized);
            EventBus.Unsubscribe<InventoryUpdatedEvent>(OnInventoryUpdated);
        }
    }
}