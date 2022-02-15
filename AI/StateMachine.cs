using UnityEngine;

namespace Platformer2D
{
    public class StateMachine
    {
        public Find FindState { get; private set; }
        public MainState MainState { get; private set; }
        //public State NextState { get; private set; }
        public MainState PreviousMainState { get; private set; }

        public void InitStartingState(MainState mainState)
        {
            MainState = mainState;
            mainState.Enter();
        }

        public void InitFindState(Find find)
        {
            FindState = find;
            find.Enter();
        }

        public void ChangeMainState(MainState newState)
        {
            MainState.Exit();
            PreviousMainState = MainState;
            MainState = newState;
            newState.Enter();
        }
    }
}