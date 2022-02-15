using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{ 
    public class AIController : MonoBehaviour, IUseController
    { 
        public UnityAction OnAttack;
        public UnityAction<bool> OnMove;
        public UnityAction OnJump;
        public UnityAction OnEnterDoor;
    }
}
