// Assets/_Project/Scripts/Services/AudioService.cs
using System.Collections.Generic;
using Core.EventBus;
using Data.ScriptableObjects;
using Modules.Bonus.Events;
using Modules.Chest.Events;
using Modules.Inventory.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services
{
    public class AudioService
    {
        private readonly AudioSource _audioSource;
        private readonly Dictionary<SoundID, SoundEntry> _soundMap;

        public AudioService(AudioConfig config, AudioSource audioSource)
        {
            _audioSource = audioSource;
            _soundMap = new Dictionary<SoundID, SoundEntry>();

            foreach (var sound in config.Sounds)
            {
                _soundMap[sound.Id] = sound;
            }

            EventBus.Subscribe<ChestSpawnedEvent>(OnChestSpawned);
            EventBus.Subscribe<ChestOpenedEvent>(OnChestOpened);
            EventBus.Subscribe<CoinCollectedEvent>(OnCoinCollected);
            EventBus.Subscribe<CrystalCollectedEvent>(OnCrystalCollected);
            EventBus.Subscribe<HealthRestoredEvent>(OnHealthCollected);
            EventBus.Subscribe<QuickCollectBonusEvent>(OnBonusActivated);
        }

        private void OnChestSpawned(ChestSpawnedEvent eventData) => Play(SoundID.ChestSpawn);
        private void OnChestOpened(ChestOpenedEvent eventData) => Play(SoundID.ChestOpen);
        private void OnCoinCollected(CoinCollectedEvent eventData) => Play(SoundID.CoinCollect);
        private void OnCrystalCollected(CrystalCollectedEvent eventData) => Play(SoundID.CrystalCollect);
        private void OnHealthCollected(HealthRestoredEvent eventData) => Play(SoundID.HealthCollect);
        private void OnBonusActivated(QuickCollectBonusEvent eventData) => Play(SoundID.BonusActivate);

        private void Play(SoundID soundId)
        {
            if (!_soundMap.TryGetValue(soundId, out SoundEntry entry)) return;
            if (entry.Clip == null) return;

            AudioClip clip = entry.Clip;
            _audioSource.pitch = Random.Range(entry.MinPitch, entry.MaxPitch);
            _audioSource.PlayOneShot(clip, entry.Volume);
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<ChestSpawnedEvent>(OnChestSpawned);
            EventBus.Unsubscribe<ChestOpenedEvent>(OnChestOpened);
            EventBus.Unsubscribe<CoinCollectedEvent>(OnCoinCollected);
            EventBus.Unsubscribe<CrystalCollectedEvent>(OnCrystalCollected);
            EventBus.Unsubscribe<HealthRestoredEvent>(OnHealthCollected);
            EventBus.Unsubscribe<QuickCollectBonusEvent>(OnBonusActivated);
        }
    }
}