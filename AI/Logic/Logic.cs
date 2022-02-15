using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{ 
    public class Logic : MonoBehaviour
    {
        protected StateMachine _stateMachine;

        private void Update()
        {
            _stateMachine.FindState.LogicUpdate();
            _stateMachine.MainState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _stateMachine.FindState.PhysicsUpdate();
            _stateMachine.MainState.PhysicsUpdate();
        }
    }
}