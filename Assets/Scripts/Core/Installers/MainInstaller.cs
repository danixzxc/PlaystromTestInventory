using Core.EventBus;
using Core.Pool;
using Data.ScriptableObjects;
using Modules.Bonus.Model;
using Modules.Bonus.Presenter;
using Modules.Bonus.View;
using Modules.Chest.Model;
using Modules.Chest.Presenter;
using Modules.Chest.Services;
using Modules.Chest.View;
using Modules.Drop.Factory;
using Modules.Drop.View;
using Modules.Inventory.Model;
using Modules.Inventory.Presenter;
using Modules.Inventory.View;
using Services;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class MainInstaller : MonoInstaller
    {
        [Header("Configs")]
        [SerializeField] private ChestConfig _chestConfig;
        [SerializeField] private InventoryConfig _inventoryConfig;
        [SerializeField] private BonusConfig _bonusConfig;

        [Header("UI Targets")]
        [SerializeField] private CoinIndicatorView _coinIndicatorView;
        [SerializeField] private CrystalIndicatorView _crystalIndicatorView;
        [SerializeField] private HealthBarView _healthBarView;

        [Header("Chest")]
        [SerializeField] private ChestView _chestView;

        [Header("Bonus")]
        [SerializeField] private BonusView _bonusView;

        [Header("Drop Prefabs")]
        [SerializeField] private BaseDropItemView _coinDropPrefab;
        [SerializeField] private BaseDropItemView _crystalDropPrefab;
        [SerializeField] private BaseDropItemView _healthDropPrefab;

        [Header("Pool Settings")]
        [SerializeField] private int _poolInitialSize = 10;
        [SerializeField] private Transform _poolParent;

        public override void InstallBindings()
        {
            BindConfigs();
            BindModels();
            BindViews();
            BindTargets();
            BindPools();
            BindServices();
            BindChestServices();
            BindFactories();
            BindPresenters();
        }

        private void BindConfigs()
        {
            Container.Bind<ChestConfig>().FromInstance(_chestConfig).AsSingle();
            Container.Bind<InventoryConfig>().FromInstance(_inventoryConfig).AsSingle();
            Container.Bind<BonusConfig>().FromInstance(_bonusConfig).AsSingle();
        }

        private void BindModels()
        {
            Container.Bind<InventoryModel>().AsSingle();
            Container.Bind<ChestModel>().AsSingle();
            Container.Bind<BonusModel>().AsSingle();
        }

        private void BindViews()
        {
            Container.Bind<CoinIndicatorView>().FromInstance(_coinIndicatorView).AsSingle();
            Container.Bind<CrystalIndicatorView>().FromInstance(_crystalIndicatorView).AsSingle();
            Container.Bind<HealthBarView>().FromInstance(_healthBarView).AsSingle();
            Container.Bind<ChestView>().FromInstance(_chestView).AsSingle();
            Container.Bind<BonusView>().FromInstance(_bonusView).AsSingle();
        }

        private void BindTargets()
        {
            Container.Bind<RectTransform>()
                .WithId("CoinTarget")
                .FromInstance(_coinIndicatorView.GetComponent<RectTransform>())
                .AsCached();

            Container.Bind<RectTransform>()
                .WithId("CrystalTarget")
                .FromInstance(_crystalIndicatorView.GetComponent<RectTransform>())
                .AsCached();

            Container.Bind<RectTransform>()
                .WithId("HealthTarget")
                .FromInstance(_healthBarView.GetComponent<RectTransform>())
                .AsCached();
        }

        private void BindPools()
        {
            Container.Bind<ObjectPool>()
                .WithId("CoinPool")
                .FromInstance(new ObjectPool(_coinDropPrefab, _poolInitialSize, _poolParent))
                .AsCached();

            Container.Bind<ObjectPool>()
                .WithId("CrystalPool")
                .FromInstance(new ObjectPool(_crystalDropPrefab, _poolInitialSize, _poolParent))
                .AsCached();

            Container.Bind<ObjectPool>()
                .WithId("HealthPool")
                .FromInstance(new ObjectPool(_healthDropPrefab, _poolInitialSize, _poolParent))
                .AsCached();
        }

        private void BindServices()
        {
            Container.Bind<AnimationService>().AsSingle();
            Container.Bind<ItemMovementService>().AsSingle();
            Container.Bind<BonusService>().AsSingle();
        }

        private void BindChestServices()
        {
            Container.Bind<ChestStateService>().AsSingle();
            Container.Bind<ChestCycleService>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<DropItemFactory>().AsSingle();
        }
        
private void BindPresenters()
        {
            Container.Bind<CoinPresenter>().AsSingle().NonLazy();
            Container.Bind<CrystalPresenter>().AsSingle().NonLazy();
            Container.Bind<HealthPresenter>().AsSingle().NonLazy();
            Container.Bind<ChestPresenter>().AsSingle().NonLazy();
            Container.Bind<BonusPresenter>().AsSingle().NonLazy();
        }
    }
}