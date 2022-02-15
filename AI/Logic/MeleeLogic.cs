using UnityEngine;

namespace Platformer2D
{
    public class MeleeLogic : Logic
    {
        [HideInInspector] public FindTarget FindTarget;
        [HideInInspector] public FindObstacle FindObstacle;
        [HideInInspector] public DisableFind DisableFind;
        [HideInInspector] public Idle Idle;
        [HideInInspector] public Jump Jump;
        [HideInInspector] public MoveToTarget MoveToTarget;
        [HideInInspector] public FightWithTarget FightWithTarget;
        [HideInInspector] public MoveToDoor MoveToDoor;
        [HideInInspector] public Freezed Freezed;
        protected StateData _stateData;
        //[HideInInspector] public SelfDefense SelfDefense;
        //[HideInInspector] public PatrolFind PatrolFind;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            UnitData unitData = GetComponent<UnitData>();
            AIController controller = GetComponent<AIController>();

            _stateData = new StateData(this, unitData, _stateMachine, controller);
        }

        private void Start()
        {
            FindTarget = new FindTarget(_stateData);
            FindObstacle = new FindObstacle(_stateData);
            DisableFind = new DisableFind(_stateData);
            Idle = new Idle(_stateData);
            Jump = new Jump(_stateData);
            MoveToTarget = new MoveToTarget(_stateData);
            FightWithTarget = new FightWithTarget(_stateData);
            MoveToDoor = new MoveToDoor(_stateData);
            //_stateData.Target = new UnitTarget(FindObjectOfType<PlayerData>(), null);

            _stateMachine.InitStartingState(Idle);

            PlayerController.Instance.OnEnterDoor += Test; //!
        }

        private void OnDisable()
        {
            PlayerController.Instance.OnEnterDoor -= Test; //!
        }

        private void Test() //!
        {
            if(_stateMachine.MainState == MoveToTarget || 
                _stateMachine.MainState == FightWithTarget)
            {
                PlayerData playerData = (_stateData.Target as UnitTarget).UnitData as PlayerData;
                _stateData.Target = new DoorTarget(playerData.CurentDoor);
                _stateMachine.ChangeMainState(MoveToDoor);
            }
        }
    }
}
