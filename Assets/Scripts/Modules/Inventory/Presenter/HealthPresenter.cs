using Modules.Inventory.Events;
using Modules.Inventory.View;
using Core.EventBus;

namespace Modules.Inventory.Presenter
{
    public class HealthPresenter
    {
        private readonly HealthBarView _view;

        public HealthPresenter(HealthBarView view)
        {
            _view = view;
            EventBus.Subscribe<InventoryUpdatedEvent>(OnInventoryUpdated);
        }

        private void OnInventoryUpdated(InventoryUpdatedEvent eventData)
        {
            _view.UpdateHealth(eventData.CurrentHealth, eventData.MaxHealth);
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<InventoryUpdatedEvent>(OnInventoryUpdated);
        }
    }
}