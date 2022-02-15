using UnityEngine;

namespace Platformer2D
{
    public class DoorTarget : ITarget
    { 
        public Door Door { get; private set; }

        public Transform DataTransform => Door.transform;

        public DoorTarget(Door door)
        {
            Door = door;
        }
    }
}
