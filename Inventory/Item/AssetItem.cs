using System;
using UnityEngine;

namespace Platformer2D
{
    [CreateAssetMenu(menuName = "Item/AssetItem")]
    public class AssetItem : ScriptableObject, IItem
    {
        public string Name => _name;
        public Sprite Sprite => _sprite;
        public GameObject Prefab => _prefab;
        public int Id => _id;

        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _id;
    }
}
