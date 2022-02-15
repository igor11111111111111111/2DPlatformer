using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public class BombData : SkillData
    {
        public UnityAction<int> OnChangeCount;
        public int Count
        { 
            get => _count;
            set
            {
                if (_count != value)
                    OnChangeCount?.Invoke(value);
                _count = value;
            }
        }
        private int _count;
        public float CurrentForce
        {
            get { return Mathf.Clamp(_currentForce, 0, _maxForce); }
        }
        private float _currentForce;
        private float _throwForcePerFrame = 0.2f;
        private float _maxForce = 20f;

        protected override void OnEnable()
        {
            base.OnEnable();
            OnStartAttack += () => _currentForce = 0;
            OnHoldAttack += () => _currentForce += _throwForcePerFrame;
            OnCancelAttack += () => Count--;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            OnStartAttack -= () => _currentForce = 0;
            OnHoldAttack -= () => _currentForce += _throwForcePerFrame;
            OnCancelAttack -= () => Count--;
        }

        protected override bool CancelAttackCondition()
        {
            return _count > 0;
        }

        protected override bool HoldAttackCondition()
        {
            return _count > 0;
        }

        protected override bool StartAttackCondition()
        {
            return _count > 0;
        }
    }
}
