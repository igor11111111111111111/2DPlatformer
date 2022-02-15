using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public class SaveGame : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Environment _environment;
        [SerializeField] private UnitsAIOnScene _unitsAIOnScene;
        [SerializeField] private FloorOnScene _floorOnScene;
        [SerializeField] private NewGame _newGame;
        [SerializeField] private BullData _bullData;
        public UnityAction OnLoad;
        private PlayerData _playerData;
        private GameJson _json;

        private void Start()
        {
            _playerData = FindObjectOfType<PlayerData>();
            _json = new GameJson();

            Load();

            MainController.Instance.OnSave += Save;
            MainController.Instance.OnLoad += Load;
            EscMenuPanel.Instance.OnNewGame += TryInitNewGame;
            EscMenuPanel.Instance.OnLastCheckpoint += Load;
        }

        private void OnDisable()
        {
            MainController.Instance.OnSave -= Save;
            MainController.Instance.OnLoad -= Load;
            EscMenuPanel.Instance.OnNewGame -= TryInitNewGame;
            EscMenuPanel.Instance.OnLastCheckpoint -= Load;
        }

        private void Save()
        {
            GameSaveData gameData = new GameSaveData();

            gameData.PlayerSaveData = new PlayerSaveData
            (
                _playerData.transform.position.ToSerializedVector(),
                _playerData.Health,
                _playerData.BombData.Count
            );
            gameData.BullSaveData = new BullSaveData
            (
                _bullData.Alive
            );
            gameData.InventoryData = new InventorySaveData
            (
                _inventory.GetItemsId()
            );
            gameData.EnvironmentData = new EnvironmentSaveData
            (
                _environment.RefreshedData()
            );
            gameData.UnitsAIData = _unitsAIOnScene.Get();
            gameData.FloorSaveData = _floorOnScene.Get();

            _json.Save(gameData);
        }

        private void Load()
        {
            GameSaveData loadGameData = new GameSaveData();
            _json.Load(loadGameData);
            if (!TryInitGameData(loadGameData))
                TryInitNewGame();
        }

        private void TryInitNewGame() 
        {
            try
            {
                TryInitGameData(_newGame.Data);
            }
            catch (System.Exception)
            {
                throw new System.Exception("Wrong NewGame.Data");
            }
            Save();
        }

        private bool TryInitGameData(GameSaveData gameData)
        {
            PlayerSaveData playerSaveData = gameData.PlayerSaveData;
            BullSaveData bullSaveData = gameData.BullSaveData;
            InventorySaveData inventoryData = gameData.InventoryData;
            EnvironmentSaveData environmentData = gameData.EnvironmentData;

            if (playerSaveData != null &&
                inventoryData.ItemsId != null &&
                environmentData.PrefabData != null)
            {
                _playerData.transform.position = playerSaveData.Position.ToVector3();
                _playerData.Health = playerSaveData.Health;
                _playerData.BombData.Count = playerSaveData.Bomb;

                _bullData.Alive = bullSaveData.Alive;
                if (!bullSaveData.Alive)//
                    _bullData.gameObject.SetActive(false);//
                else
                    _bullData.gameObject.SetActive(true);//

                _inventory.SetItems(inventoryData.ItemsId);

                _environment.InitData(new List<PrefabSaveData>(environmentData.PrefabData));

                _unitsAIOnScene.Set(gameData.UnitsAIData);
                _floorOnScene.Set(gameData.FloorSaveData);

                OnLoad?.Invoke();
                return true;
            }
            return false;
        }
    }
}
