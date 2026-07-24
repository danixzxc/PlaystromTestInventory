using Data.ScriptableObjects;
using Modules.Bonus.Events;
using Core.EventBus;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

namespace Services
{
    public class BonusService
    {
        private readonly BonusConfig _bonusConfig;
        private readonly Queue<CollectionRecord> _recentCollections = new Queue<CollectionRecord>();
        private CancellationTokenSource _bonusCts;

        public BonusService(BonusConfig bonusConfig)
        {
            _bonusConfig = bonusConfig;
        }

        public void RegisterCollection(DropItemType type)
        {
            _recentCollections.Enqueue(new CollectionRecord
            {
                Type = type,
                Time = UnityEngine.Time.time
            });

            while (_recentCollections.Count > _bonusConfig.RequiredItemsForBonus)
            {
                _recentCollections.Dequeue();
            }

            CheckForBonus();
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
                    ActivateBonus();
                    _recentCollections.Clear();
                    return;
                }
            }
        }

        private async void ActivateBonus()
        {
            _bonusCts?.Cancel();
            _bonusCts = new CancellationTokenSource();

            EventBus.Fire(new QuickCollectBonusEvent(_bonusConfig.BonusMultiplier));

            try
            {
                await UniTask.Delay(
                    UnityEngine.TimeSpan.FromSeconds(_bonusConfig.BonusDurationSeconds),
                    cancellationToken: _bonusCts.Token
                );
                DeactivateBonus();
            }
            catch (OperationCanceledException) { }
        }

        private void DeactivateBonus()
        {
            EventBus.Fire(new QuickCollectBonusEvent(1f));
        }

        private struct CollectionRecord
        {
            public DropItemType Type;
            public float Time;
        }
    }
}