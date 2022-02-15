using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public class MainController : MonoBehaviour 
    {
        public static MainController Instance;

        public UnityAction OnLoad;
        public UnityAction OnSave;
        public UnityAction<bool> OnEsc;
        public UnityAction<bool> OnInventory;

        [SerializeField] private InventoryPanel _inventory;
        [SerializeField] private PlayerData _playerData;
        private CustomInput _input;
        private CustomInput.MainActions _actions => _input.Main;

        private void Awake()
        {
            _input = new CustomInput();
        }

        private void OnEnable()
        {
            Instance = this;
            _input.Enable();

            _actions.Save.started += _ =>
            {
                if (PlayerEnvironmentCheck.Instance.TryGet<CheckPoint>())
                {
                    OnSave?.Invoke();
                }
            };

            _actions.Load.started += _ =>
            {
                OnLoad?.Invoke();
            };

            _actions.Escape.started += _ =>
            {
                if (!EscMenuPanel.Instance.IsActiveBody && _inventory.IsActiveBody)
                {
                    OnInventory?.Invoke(false);
                } 
                OnEsc?.Invoke(!EscMenuPanel.Instance.IsActiveBody);
            };

            _actions.Inventory.started += _ =>
            {
                if(!EscMenuPanel.Instance.IsActiveBody && _playerData.Health != 0)
                    OnInventory?.Invoke(!_inventory.IsActiveBody);
            };
        }

        private void OnDisable()
        {
            _input.Disable();
        }
    }
}

