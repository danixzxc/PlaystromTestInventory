using Core.EventBus;
using Modules.Chest.Events;
using Modules.Chest.Model;
using Modules.Chest.Services;
using Modules.Inventory.Events;
using Zenject;

namespace Modules.Chest.Presenter
{
    public class ChestPresenter
    {
        private readonly ChestModel _model;
        private readonly ChestStateService _chestStateService;
        private readonly ChestCycleService _chestCycleService;

        public ChestPresenter(
            ChestModel model,
            ChestStateService chestStateService,
            ChestCycleService chestCycleService)
        {
            _model = model;
            _chestStateService = chestStateService;
            _chestCycleService = chestCycleService;
        }

        [Inject]
        private void Init()
        {
            _chestCycleService.StartCycle();
        }
    }
}