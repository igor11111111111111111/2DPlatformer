using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class SkeletMeleeData : AIData
    {
        public override int Health
        {
            get
            {
                return _health;
            }
            set
            {
                var clampedhealth = Mathf.Clamp(value, 0, _maxHealth);
                if (clampedhealth == 0)
                    OnDeath?.Invoke();
                _health = clampedhealth;
            }
        }
        private int _health;
        public override int Team => _team;
        private int _team;
        public override int MaxHealth => _maxHealth; 
        private int _maxHealth;
        public override int Damage => _damage;
        private int _damage;
        public override int DelayAttack => _delayAttack; 
        private int _delayAttack;
        public const float DEFAULT_SPEED = 2f;
        public const float DEFAULT_JUMP_FORCE = 24f;

        protected override void Awake()
        {
            base.Awake();
            _maxHealth = 100;
            _team = 2;
            _damage = 1;
            _delayAttack = 60;
        }
    }
}
