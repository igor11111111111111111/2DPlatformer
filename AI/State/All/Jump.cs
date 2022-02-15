using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class Jump : MainState
    {
        public Jump(StateData stateData) : base(stateData)
        {
        }

        public override void Enter()
        {
            _data.StateMachine.InitFindState((_data.Logic as MeleeLogic).DisableFind);
            _data.Controller.OnJump?.Invoke();
            _data.JumpCD.Passed = false;
            _data.StateMachine.ChangeMainState(_data.StateMachine.PreviousMainState);
        }
    }
}
