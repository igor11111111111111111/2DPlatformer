using UnityEngine;

namespace Platformer2D
{
    public class DisableFind : Find
    { 
        public DisableFind(StateData stateData) : base(stateData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            foreach (var line in _data.Eyes.Lines)
            {
                line.SetPosition(1, line.GetPosition(0));
            }
        }
    }
}
