using UnityEngine;

namespace Platformer2D
{
    public abstract class FloorData : MonoBehaviour
    {
        protected FloorOnScene _floorOnScene;

        public virtual void InitFloorOnScene(FloorOnScene floorOnScene)
        {
            _floorOnScene = floorOnScene;
        }
    }
}