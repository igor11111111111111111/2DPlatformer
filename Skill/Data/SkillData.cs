using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public abstract class SkillData : MonoBehaviour
    {
        public UnityAction OnStartAttack;
        private UnityAction _onStartAttack;

        public UnityAction OnHoldAttack;
        private UnityAction _onHoldAttack;

        public UnityAction OnCancelAttack;
        private UnityAction _onCancelAttack;

        public UnityAction OnInitSkill;
        public UnityAction OnDeInitSkill;

        protected virtual void OnEnable()
        {
            _onStartAttack += () => { if (StartAttackCondition()) OnStartAttack?.Invoke(); };
            _onHoldAttack += () => { if (HoldAttackCondition()) OnHoldAttack?.Invoke(); };
            _onCancelAttack += () => { if (CancelAttackCondition()) OnCancelAttack?.Invoke(); };
        }

        protected virtual void OnDisable()
        {
            _onStartAttack -= () => { if (StartAttackCondition()) OnStartAttack?.Invoke(); };
            _onHoldAttack -= () => { if (HoldAttackCondition()) OnHoldAttack?.Invoke(); };
            _onCancelAttack -= () => { if (CancelAttackCondition()) OnCancelAttack?.Invoke(); };
        }
         
        protected abstract bool StartAttackCondition();
        protected abstract bool HoldAttackCondition();
        protected abstract bool CancelAttackCondition();

        public void InitActions(ref UnityAction start, ref UnityAction hold, ref UnityAction cancel)
        {
            start = _onStartAttack;
            hold = _onHoldAttack;
            cancel = _onCancelAttack;
        }
    }
}
