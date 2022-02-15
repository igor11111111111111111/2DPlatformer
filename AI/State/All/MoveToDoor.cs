using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class MoveToDoor : MainState
    {
        public MoveToDoor(StateData stateData) : base(stateData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _data.StateMachine.InitFindState((_data.Logic as MeleeLogic).FindObstacle);
        }

        public override void LogicUpdate()
        {
            _data.UnitData.SetTargetDirection(_data.Target);
            var distance = _data.ContactCheck.GetDistance(_data.Target.DataTransform);
            if (Mathf.Abs(distance.x) > 0.2f && Mathf.Abs(distance.x) < 10)
            {
                _data.Controller.OnMove?.Invoke(true);
            }
            else if (Mathf.Abs(distance.x) <= 0.4f && distance.y >= -0.6f && distance.y <= -0.2f)
            {
                _data.UnitData.CurentDoor = (_data.Target as DoorTarget).Door;
                _data.Controller.OnEnterDoor?.Invoke();

                var freezed = (_data.Logic as MeleeLogic).Freezed = new Freezed(_data, 90);
                _data.StateMachine.ChangeMainState(freezed);
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

