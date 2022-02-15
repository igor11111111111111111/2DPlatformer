using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public class FloorOnScene : MonoBehaviour
    {
        [SerializeField] private List<FloorData> _floorDatas;
        [SerializeField] private FloorDictionary _floorDictionary;
        public UnityAction OnComplete;

        public List<FloorSaveData> Get()
        {
            List<FloorSaveData> _floorSaveDatas = new List<FloorSaveData>();
            foreach (var data in _floorDatas)
            {
                string id = data.GetType().Name.Replace("Data", "");
                SerializedVector3 position = data.transform.position.ToSerializedVector();
                var floor = new FloorSaveData(id, position);
                _floorSaveDatas.Add(floor);
            }
            return _floorSaveDatas;
        }

        public void Set(List<FloorSaveData> floorSaveDatas)
        {
            Clear();

            foreach (var floorSaveData in floorSaveDatas)
            {
                FloorData original = _floorDictionary.KeyToValue(floorSaveData.Id);
                Vector3 position = floorSaveData.Position.ToVector3();
                FloorData data = Instantiate(original, position, Quaternion.identity, transform);
                //data.OnDeath += () => Death(data);
                data.InitFloorOnScene(this);
                _floorDatas.Add(data);
            }

            OnComplete?.Invoke();
        }

        //private void Death(AIData data)
        //{
        //    _floorDatas.Remove(data);
        //    data.OnDeath -= () => Death(data);
        //}

        private void Clear()
        {
            _floorDatas?.Clear();

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
} 
