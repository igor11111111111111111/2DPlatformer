namespace Platformer2D
{
    public class Idle : MainState
    {
        public Idle(StateData stateData) : base(stateData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _data.StateMachine.InitFindState((_data.Logic as MeleeLogic).FindTarget);
        }
    }
}
