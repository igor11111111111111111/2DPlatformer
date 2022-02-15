using UnityEngine;

namespace Platformer2D
{
    [CreateAssetMenu(menuName = "FloorSO")]
    public class FloorSO : ScriptableObject
    {
        public string Id => _id;
        [SerializeField] private string _id;
        public FloorData Data => _data;
        [SerializeField] private FloorData _data;
    }
}
