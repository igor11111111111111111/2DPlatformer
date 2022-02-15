using UnityEngine;

namespace Platformer2D
{ 
    public class UnitTarget : ITarget
    {
        public UnitData UnitData { get; private set; }
        public Collider2D Collider { get; private set; }

        public Transform DataTransform => UnitData.transform;

        public UnitTarget(UnitData unitData, Collider2D collider)
        {
            UnitData = unitData; 
            Collider = collider;
        }
    }
}
