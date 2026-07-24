using Modules.Bonus.Events;
using Modules.Bonus.Model;
using Core.EventBus;
using DG.Tweening;
using UnityEngine;

namespace Modules.Bonus.Presenter
{
    public class BonusPresenter
    {
        private readonly BonusModel _model;

        public BonusPresenter(BonusModel model)
        {
            _model = model;
            EventBus.Subscribe<QuickCollectBonusEvent>(OnBonusActivated);
        }

        private void OnBonusActivated(QuickCollectBonusEvent eventData)
        {
            _model.ActivateBonus(eventData.Multiplier);
            Debug.Log($"Bonus activated! Multiplier: {eventData.Multiplier}");
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<QuickCollectBonusEvent>(OnBonusActivated);
        }
    }
}