using UnityEngine;

namespace Platformer2D
{
    [CreateAssetMenu(menuName = "UnitAISO")]
    public class UnitAISO : ScriptableObject
    {
        public string Id => _id;
        [SerializeField] private string _id;
        public AIData AIData => _aIData;
        [SerializeField] private AIData _aIData;
    }
}
