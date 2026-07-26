using Modules.Inventory.Events;
using Modules.Inventory.View;

namespace Modules.Inventory.Presenter
{
    public class CrystalPresenter : BaseResourcePresenter<CrystalIndicatorView>
    {
        public CrystalPresenter(CrystalIndicatorView view) : base(view) { }

        protected override void OnInventoryInitialized(InventoryInitializedEvent eventData)
        {
            _view.Initialize(eventData.Crystals);
        }

        protected override void OnInventoryUpdated(InventoryUpdatedEvent eventData)
        {
            _view.UpdateAmount(eventData.Crystals);
        }
    }
}