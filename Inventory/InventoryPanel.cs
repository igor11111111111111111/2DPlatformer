using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class InventoryPanel : MonoBehaviour
    { 
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventoryCell _inventoryCellTemplate;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _draggingParent;
        [SerializeField] private ItemsEjector _ejector;
        [SerializeField] private GameObject _body;
        [SerializeField] private SaveGame _saveGame;

        public bool IsActiveBody => _body.activeInHierarchy;

        private void Start()
        {
            SetBodyActive(false);
            _saveGame.OnLoad += Render;
            MainController.Instance.OnInventory += SetBodyActive;
        }

        private void SetBodyActive(bool active)
        {
            _body.SetActive(active);
        }
         
        public void Render()
        {
            DestroyChilds();

            foreach (var item in _inventory.GetItems())
            {
                var cell = Instantiate(_inventoryCellTemplate, _container);
                cell.Init(_draggingParent);
                cell.Render(item);

                cell.OnEject += () =>
                {
                    _inventory.Remove(item);
                    Destroy(cell.gameObject);
                    _ejector.Eject(item);
                };
            }
        }

        private void DestroyChilds()
        {
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

