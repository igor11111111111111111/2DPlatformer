using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Platformer2D
{
    public class Inventory : MonoBehaviour
    {
        private List<IItem> _items;
        [SerializeField] private ItemsDictionary _itemsDictionary;
        [SerializeField] private InventoryPanel _inventoryPanel;

        public List<int> GetItemsId()
        {
            List<int> ids = new List<int>();
            foreach (var item in _items)
            {
                ids.Add(item.Id);
            }
            return ids;
        }

        public ReadOnlyCollection<IItem> GetItems()
        {
            return _items.AsReadOnly();
        }

        public void Add(int id)
        {
            _items.Add(_itemsDictionary.KeyToValue(id));
            _inventoryPanel.Render();
        }

        public void SetItems(List<int> id)
        {
            _items = _itemsDictionary.KeysToValues(id);
        }

        public void Remove(IItem item)
        {
            _items.Remove(item);
            _inventoryPanel.Render();
        }
    }
}
