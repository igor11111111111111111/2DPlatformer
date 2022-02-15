using UnityEngine;

namespace Platformer2D
{
    public class Freezed : MainState
    {

        public Freezed(StateData stateData, int delay) : base(stateData)
        {
            _delayCustomUpdate = delay;
        } 

        public override void Enter()
        {
            _time = 1;
            _data.StateMachine.InitFindState((_data.Logic as MeleeLogic).DisableFind);
        }

        public override void CustomUpdate()
        {
            _data.StateMachine.ChangeMainState((_data.Logic as MeleeLogic).Idle);
        }
    }
}
