using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public abstract class ThinkData : MonoBehaviour
    {
        public UnityAction<int> OnTakeDamage;
    }
}
