using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Platformer2D
{
    public class Environment : MonoBehaviour
    {
        private List<PrefabSaveData> _prefabData;
        [SerializeField] private ItemsDictionary _itemsDictionary;
        [SerializeField] private SaveGame _saveGame;

        private void Awake()
        {
            _saveGame.OnLoad += RespawnPrefabs;
        }

        public void InitData(List<PrefabSaveData> data)
        {
            _prefabData = data;
        }

        public void Remove(PickableObject pickableObject)
        {
            var data = _prefabData.Where(d => d == pickableObject.PrefabData).FirstOrDefault();
            _prefabData.Remove(data);
        }

        public PrefabSaveData Add(IItem item, GameObject prefab)
        {
            SerializedVector3 position = prefab.transform.position.ToSerializedVector();
            PrefabSaveData data = new PrefabSaveData(item.Id, position, prefab);
            _prefabData.Add(data);
            return data;
        }

        public List<PrefabSaveData> RefreshedData()
        {
            _prefabData.ForEach(prefabData =>
            {
                prefabData.SetPosition();
            });
            return _prefabData;
        }

        private void RespawnPrefabs()
        {
            DestroyChilds();

            _prefabData.ForEach(prefabData =>
            {
                IItem item = _itemsDictionary.KeyToValue(prefabData.Id);
                Vector3 position = prefabData.Position.ToVector3();
                
                prefabData.GameObject = Instantiate(item.Prefab, position, Quaternion.identity, transform);
                InitPrefabItem(prefabData, item);    
            });
        }

        private void InitPrefabItem(PrefabSaveData prefabData, IItem item)
        {
            if (prefabData.GameObject.TryGetComponent(out IPickable pickable))
            {
                pickable.Id = item.Id;
                pickable.PrefabData = prefabData;
            }
        }

        private void DestroyChilds()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

