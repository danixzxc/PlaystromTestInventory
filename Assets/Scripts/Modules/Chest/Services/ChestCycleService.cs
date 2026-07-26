using Core.EventBus;
using Data.ScriptableObjects;
using DG.Tweening;
using Modules.Chest.Events;
using Modules.Chest.View;
using UnityEngine;

namespace Modules.Chest.Services
{
    public class ChestCycleService
    {
        private readonly ChestStateService _chestStateService;
        private readonly ChestView _chestView;
        private readonly float _respawnDelay;

        public ChestCycleService(ChestStateService chestStateService, ChestView chestView,
            ChestConfig chestConfig)
        {
            _chestStateService = chestStateService;
            _chestView = chestView;
            _respawnDelay = chestConfig.RespawnDelay;
            EventBus.Subscribe<ChestCollectedAllDropsEvent>(OnAllDropsCollected);
        }

        public void StartCycle()
        {
            _chestStateService.RequestSpawnChest(_chestView.transform.position);
        }

        private void OnAllDropsCollected(ChestCollectedAllDropsEvent eventData)
        {
            DOVirtual.DelayedCall(_respawnDelay, () =>
            {
                _chestStateService.RequestSpawnChest(_chestView.transform.position);
            });
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<ChestCollectedAllDropsEvent>(OnAllDropsCollected);
        }
    }
}