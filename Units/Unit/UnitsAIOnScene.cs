using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class UnitsAIOnScene : MonoBehaviour 
    {
        [SerializeField] private List<AIData> _aIDatas;
        [SerializeField] private UnitsAIDictionary _unitsDictionary;

        public List<UnitAISaveData> Get()
        {
            List<UnitAISaveData> _unitAISaveDatas = new List<UnitAISaveData>();
            foreach (var aiData in _aIDatas)
            {
                string id = aiData.GetType().Name.Replace("Data", "");
                SerializedVector3 position = aiData.transform.position.ToSerializedVector();
                var _unit = new UnitAISaveData(id, position, aiData.Health);
                _unitAISaveDatas.Add(_unit);
            }
            return _unitAISaveDatas;
        }

        public void Set(List<UnitAISaveData> unitAISaveDatas)
        {
            Clear();

            foreach (var unitSaveData in unitAISaveDatas)
            {
                AIData original = _unitsDictionary.KeyToValue(unitSaveData.Id);
                Vector3 position = unitSaveData.Position.ToVector3();
                AIData data = Instantiate(original, position, Quaternion.identity, transform);
                data.Health = unitSaveData.Health;
                data.OnDeath += () => Death(data);
                _aIDatas.Add(data);
            }
        }

        private void Death(AIData data)
        {
            _aIDatas.Remove(data);
            data.OnDeath -= () => Death(data);
        }

        private void Clear()
        {
            _aIDatas?.Clear();

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
