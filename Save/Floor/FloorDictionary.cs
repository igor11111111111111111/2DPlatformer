using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class FloorDictionary : MonoBehaviour
    {
        private Dictionary<string, FloorData> _dictionary;

        private void Awake()
        {
            Fill();
        }
         
        private void Fill()
        {
            _dictionary = new Dictionary<string, FloorData>();

            var soArray = Resources.LoadAll<ScriptableObject>("Floor");
            foreach (var so in soArray)
            {
                var floor = so as FloorSO;
                _dictionary.Add(floor.Id, floor.Data);
            }
        }

        public FloorData KeyToValue(string id)
        {
            FloorData data;
            _dictionary.TryGetValue(id, out data);
            return data;
        }
    }
}
