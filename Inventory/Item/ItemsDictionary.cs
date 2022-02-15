using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class ItemsDictionary : MonoBehaviour
    {
        private Dictionary<int, IItem> _dictionary;

        private void Awake()
        {
            Fill();
        }

        public List<IItem> KeysToValues(List<int> ids)
        {
            List<IItem> items = new List<IItem>();
            ids.ForEach(id =>
            {
                IItem item;
                _dictionary.TryGetValue(id, out item);
                items.Add(item);
            });
            return items;
        }

        public IItem KeyToValue(int id)
        {
            IItem item;
            _dictionary.TryGetValue(id, out item);
            return item;
        }

        private void Fill()
        {
            _dictionary = new Dictionary<int, IItem>();

            var soArray = Resources.LoadAll<ScriptableObject>("Items");
            foreach (var so in soArray)
            { 
                var item = so as IItem;
                _dictionary.Add(item.Id, item);
            }
        }
    }
}
