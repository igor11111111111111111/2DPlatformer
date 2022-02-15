using UnityEngine;
using UnityEngine.AI;

namespace Platformer2D
{
    public abstract class State
    {
        protected StateData _data;// any state can add new data
        protected int _time;
        protected int _delayCustomUpdate; // 60 = one seconds;

        protected State(StateData stateData)
        {
            _data = stateData;
        }

        public virtual void Enter()
        {
            _time = -1;
            _delayCustomUpdate = 60;
        }

        public virtual void HandleInput()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {
            _time++;
            if (_time % _delayCustomUpdate == 0)
            {
                CustomUpdate();
            }
        }

        public virtual void CustomUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}
