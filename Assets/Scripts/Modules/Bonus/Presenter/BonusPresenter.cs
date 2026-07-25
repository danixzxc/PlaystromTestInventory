using Modules.Bonus.Events;
using Modules.Bonus.Model;
using Modules.Bonus.View;
using Core.EventBus;
using UnityEngine;

namespace Modules.Bonus.Presenter
{
    public class BonusPresenter
    {
        private readonly BonusModel _model;
        private readonly BonusView _view;

        public BonusPresenter(BonusModel model, BonusView view)
        {
            _model = model;
            _view = view;
            EventBus.Subscribe<QuickCollectBonusEvent>(OnBonusActivated);
            EventBus.Subscribe<BonusTimerTickEvent>(OnTimerTick);
            EventBus.Subscribe<BonusDeactivatedEvent>(OnBonusDeactivated);
        }

        private void OnBonusActivated(QuickCollectBonusEvent eventData)
        {
            _model.ActivateBonus(eventData.Multiplier, eventData.Duration, eventData.BonusType);
            _view.ShowBonus(eventData.Duration, eventData.BonusType);
        }

        private void OnTimerTick(BonusTimerTickEvent eventData)
        {
            _model.UpdateRemainingTime(eventData.DeltaTime);
            _view.UpdateTimer(_model.GetProgress());
        }

        private void OnBonusDeactivated(BonusDeactivatedEvent eventData)
        {
            _model.DeactivateBonus();
            _view.HideBonus();
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<QuickCollectBonusEvent>(OnBonusActivated);
            EventBus.Unsubscribe<BonusTimerTickEvent>(OnTimerTick);
            EventBus.Unsubscribe<BonusDeactivatedEvent>(OnBonusDeactivated);
        }
    }
}