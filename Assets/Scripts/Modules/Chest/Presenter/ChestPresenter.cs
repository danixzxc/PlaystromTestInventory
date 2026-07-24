using Modules.Chest.Events;
using Modules.Chest.Model;
using Modules.Chest.View;
using Core.EventBus;
using Data.ScriptableObjects;
using System.Collections.Generic;
using Modules.Drop.Factory;
using UnityEngine;

namespace Modules.Chest.Presenter
{
    public class ChestPresenter
    {
        private readonly ChestView _view;
        private readonly ChestModel _model;
        private readonly DropItemFactory _dropItemFactory;

        public ChestPresenter(ChestView view, ChestModel model, DropItemFactory dropItemFactory)
        {
            _view = view;
            _model = model;
            _dropItemFactory = dropItemFactory;
            _view.OnChestClicked.AddListener(OnChestClicked);
        }

        private void OnChestClicked()
        {
            _view.Open();
            List<DropItemConfig> drops = _model.GetRandomDrops();
            Vector3 chestPosition = _view.transform.position;

            foreach (var dropConfig in drops)
            {
                Vector3 spawnPosition = chestPosition + new Vector3(
                    UnityEngine.Random.Range(-1f, 1f),
                    UnityEngine.Random.Range(0.5f, 1.5f),
                    0f
                );
                _dropItemFactory.CreateDrop(dropConfig, spawnPosition);
            }

            EventBus.Fire(new ChestOpenedEvent());
        }

        public void Dispose()
        {
            _view.OnChestClicked.RemoveListener(OnChestClicked);
        }
    }
}