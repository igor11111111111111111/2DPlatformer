using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class UnitsAIDictionary : MonoBehaviour
    {
        private Dictionary<string, AIData> _dictionary;

        private void Awake()
        {
            Fill();
        }
         
        private void Fill()
        {
            _dictionary = new Dictionary<string, AIData>();

            var soArray = Resources.LoadAll<ScriptableObject>("UnitsAI");
            foreach (var so in soArray)
            {
                var unit = so as UnitAISO;
                _dictionary.Add(unit.Id, unit.AIData);
            }
        }

        public AIData KeyToValue(string id)
        {
            AIData aIData;
            _dictionary.TryGetValue(id, out aIData);
            return aIData;
        }
    }
}
