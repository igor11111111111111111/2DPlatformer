namespace Platformer2D
{
    public class StateData
    {  
        public Logic Logic;
        public UnitData UnitData;
        public ITarget Target;
        public StateMachine StateMachine;
        public AIController Controller;
        public AIContactCheck ContactCheck;
        public AIEyes Eyes;
        public AIJumpCD JumpCD;

        public StateData(Logic logic, UnitData unitData, StateMachine stateMachine, AIController controller)
        {
            Logic = logic;
            UnitData = unitData;
            StateMachine = stateMachine;
            Controller = controller;
            //Target = new ITarget();
            ContactCheck = new AIContactCheck(this);
            Eyes = unitData.GetComponentInChildren<AIEyes>();
            JumpCD = unitData.GetComponent<AIJumpCD>();
        }
    }
}
