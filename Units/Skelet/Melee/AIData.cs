using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public abstract class AIData : UnitData
    {
        public UnityAction<float> OnDirectionXchanged;
        public abstract int DelayAttack { get; }
        public override void SetTargetDirection(ITarget target)
        {
            base.SetTargetDirection(target);
            OnDirectionXchanged?.Invoke(Direction.x);
        }
    }
}
