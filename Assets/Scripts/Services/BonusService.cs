using Data.ScriptableObjects;
using Modules.Bonus.Events;
using Core.EventBus;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

namespace Services
{
    public class BonusService
    {
        private readonly BonusConfig _bonusConfig;
        private readonly Queue<CollectionRecord> _recentCollections = new Queue<CollectionRecord>();
        private CancellationTokenSource _bonusCts;
        private bool _isBonusActive;

        public BonusService(BonusConfig bonusConfig)
        {
            _bonusConfig = bonusConfig;
        }

        public void RegisterCollection(DropItemType type)
        {
            if (_isBonusActive) return;

            _recentCollections.Enqueue(new CollectionRecord
            {
                Type = type,
                Time = Time.time
            });

            while (_recentCollections.Count > _bonusConfig.RequiredItemsForBonus)
            {
                _recentCollections.Dequeue();
            }

            CheckForBonus();
        }

        public void DeactivateBonus()
        {
            _bonusCts?.Cancel();
            _isBonusActive = false;
            EventBus.Fire(new BonusDeactivatedEvent());
        }

        private void CheckForBonus()
        {
            if (_recentCollections.Count < _bonusConfig.RequiredItemsForBonus)
            {
                return;
            }

            CollectionRecord[] records = _recentCollections.ToArray();
            float timeWindow = _bonusConfig.TimeWindowSeconds;

            for (int i = 0; i <= records.Length - _bonusConfig.RequiredItemsForBonus; i++)
            {
                bool allSameType = true;
                float windowStart = records[i].Time;

                for (int j = i + 1; j < i + _bonusConfig.RequiredItemsForBonus; j++)
                {
                    if (records[j].Type != records[i].Type ||
                        records[j].Time - windowStart > timeWindow)
                    {
                        allSameType = false;
                        break;
                    }
                }

                if (allSameType)
                {
                    ActivateBonus(records[i].Type);
                    _recentCollections.Clear();
                    return;
                }
            }
        }

        private async void ActivateBonus(DropItemType type)
        {
            _isBonusActive = true;
            _bonusCts?.Cancel();
            _bonusCts = new CancellationTokenSource();

            EventBus.Fire(new QuickCollectBonusEvent(_bonusConfig.BonusMultiplier, _bonusConfig.BonusDurationSeconds, type));

            float elapsedTime = 0f;
            while (elapsedTime < _bonusConfig.BonusDurationSeconds)
            {
                float deltaTime = Time.deltaTime;
                elapsedTime += deltaTime;
                EventBus.Fire(new BonusTimerTickEvent(deltaTime));

                try
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(Time.deltaTime), cancellationToken: _bonusCts.Token);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }

            DeactivateBonus();
        }

        private struct CollectionRecord
        {
            public DropItemType Type;
            public float Time;
        }
    }
}