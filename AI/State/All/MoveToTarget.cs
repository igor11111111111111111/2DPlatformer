using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class MoveToTarget : MainState
    {
        public MoveToTarget(StateData stateData) : base(stateData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _data.UnitData.SetTargetDirection(_data.Target);
            _data.StateMachine.InitFindState((_data.Logic as MeleeLogic).FindObstacle);
        }

        public override void LogicUpdate()
        {
            if ((_data.Target as UnitTarget).UnitData.Alive)
            {
                _data.UnitData.SetTargetDirection(_data.Target);
                var distance = _data.ContactCheck.GetDistance(_data.Target.DataTransform);
                if (Mathf.Abs(distance.x) > 1 && Mathf.Abs(distance.x) < 10)
                {
                    _data.Controller.OnMove?.Invoke(true);
                }
                else if (Mathf.Abs(distance.x) <= 1 && Mathf.Abs(distance.y) <= 1.3f)
                {
                    _data.StateMachine.ChangeMainState((_data.Logic as MeleeLogic).FightWithTarget);
                }
                else
                {
                    _data.StateMachine.ChangeMainState((_data.Logic as MeleeLogic).Idle);
                }
            }
            else
            {
                _data.StateMachine.ChangeMainState((_data.Logic as MeleeLogic).Idle);
            }
        }

        public override void Exit()
        {
            _data.Controller.OnMove?.Invoke(false);
        }
    }
}
