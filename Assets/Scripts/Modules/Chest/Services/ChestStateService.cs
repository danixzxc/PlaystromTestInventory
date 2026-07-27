using Core.EventBus;
using Modules.Chest.Events;
using Modules.Chest.Model;
using Modules.Chest.View;
using Data.ScriptableObjects;
using Modules.Drop.Factory;
using Modules.Drop.View;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Chest.Services
{
    public class ChestStateService
    {
        private readonly ChestModel _model;
        private readonly ChestView _view;
        private readonly DropItemFactory _dropItemFactory;
        private readonly List<BaseDropItemView> _activeDrops = new List<BaseDropItemView>();

        public ChestStateService(ChestModel model, ChestView view, DropItemFactory dropItemFactory)
        {
            _model = model;
            _view = view;
            _dropItemFactory = dropItemFactory;

            _view.OnChestSpawnStarted.AddListener(OnSpawnStarted);
            _view.OnChestClicked.AddListener(OnChestClicked);
            _view.OnChestOpenAnimationPeak.AddListener(OnOpenAnimationPeak);
            _view.OnSpawnComplete.AddListener(OnSpawnComplete);
        }

        public void RequestSpawnChest(Vector3 position)
        {
            if (_model.CurrentState != ChestState.Inactive && _model.CurrentState != ChestState.Disappearing)
            {
                return;
            }

            _model.SetState(ChestState.Spawning);
            _view.PlaySpawnAnimation(position);
        }
        private void OnSpawnStarted()
        {
            EventBus.Fire(new ChestSpawnedEvent());
        }

        private void OnSpawnComplete()
        {
            _model.SetState(ChestState.Idle);
            _view.ShowIdleState();
            EventBus.Fire(new ChestReadyToOpenEvent());
        }

        private void OnChestClicked()
        {
            if (_model.CurrentState != ChestState.Idle)
            {
                return;
            }

            _model.SetState(ChestState.Opened);
            _view.PlayOpenAnimation();
            EventBus.Fire(new ChestOpenedEvent());
        }

        private void OnOpenAnimationPeak()
        {
            List<DropItemConfig> drops = _model.GetRandomDrops();
            _model.SetDropsCount(drops.Count);

            Vector3 chestPosition = _view.transform.position;

            foreach (var dropConfig in drops)
            {
                Vector3 spawnPosition = chestPosition + Vector3.up;
                BaseDropItemView dropView = null;
                dropView = _dropItemFactory.CreateDrop(dropConfig, spawnPosition, () => RegisterDropCollected(dropView));
                _activeDrops.Add(dropView);
            }
        }

        private void RegisterDropCollected(BaseDropItemView dropView)
        {
            _activeDrops.Remove(dropView);
            _model.RegisterDropCollected();

            if (_activeDrops.Count == 0 && _model.AllDropsCollected())
            {
                OnAllDropsCollected();
            }
        }

        private void OnAllDropsCollected()
        {
            _model.SetState(ChestState.Collecting);
            EventBus.Fire(new ChestCollectedAllDropsEvent());

            _model.SetState(ChestState.Disappearing);
            _view.PlayDisappearAnimation();
        }

        public void Dispose()
        {
            _view.OnChestClicked.RemoveListener(OnChestClicked);
            _view.OnChestOpenAnimationPeak.RemoveListener(OnOpenAnimationPeak);
            _view.OnSpawnComplete.RemoveListener(OnSpawnComplete);
        }
    }
}