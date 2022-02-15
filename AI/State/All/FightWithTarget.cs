using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class FightWithTarget : MainState
    {
        public FightWithTarget(StateData stateData) : base(stateData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _delayCustomUpdate = (_data.UnitData as AIData).DelayAttack;
            _data.StateMachine.InitFindState((_data.Logic as MeleeLogic).DisableFind);
        }

        public override void CustomUpdate()
        {
            _data.UnitData.SetTargetDirection(_data.Target);
            var distance = _data.ContactCheck.GetDistance(_data.Target.DataTransform);
            if (!(_data.Target as UnitTarget).UnitData.Alive)
            {
                _data.StateMachine.ChangeMainState((_data.Logic as MeleeLogic).Idle);
            }
            else if (Mathf.Abs(distance.x) <= 1 && Mathf.Abs(distance.y) <= 1.3f)
            {
                _data.Controller.OnAttack?.Invoke();
            }
            else
            {
                _data.StateMachine.ChangeMainState((_data.Logic as MeleeLogic).MoveToTarget);
            }
        }
    }
}
