using Modules.Inventory.Events;
using Modules.Inventory.View;

namespace Modules.Inventory.Presenter
{
    public class CoinPresenter : BaseResourcePresenter<CoinIndicatorView>
    {
        public CoinPresenter(CoinIndicatorView view) : base(view) { }

        protected override void OnInventoryInitialized(InventoryInitializedEvent eventData)
        {
            _view.Initialize(eventData.Coins);
        }

        protected override void OnInventoryUpdated(InventoryUpdatedEvent eventData)
        {
            _view.UpdateAmount(eventData.Coins);
        }
    }
}