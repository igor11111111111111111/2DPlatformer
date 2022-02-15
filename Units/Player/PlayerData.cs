using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public class PlayerData : UnitData
    {    
        private PlayerController _controller => PlayerController.Instance;

        public UnityAction<int> OnHealthChanged;
        public UnityAction<bool> OnFreezed;
        public UnityAction OnRessurect;

        public FistData FistData => _fistData;
        [SerializeField] private FistData _fistData;
        public BombData BombData => _bombData;
        [SerializeField] private BombData _bombData;
        public TommyGunData TommyGunData => _tommyGunData;
        [SerializeField] private TommyGunData _tommyGunData;

        [HideInInspector] public bool UnderRoof;
        [HideInInspector] public float CurrentSpeed;
        [HideInInspector] public float CurrentJumpForce;
        [HideInInspector] public bool IsUnderImmunity
        {
            get
            {
                return _isUnderImmunity;
            }
            set
            {
                if (value)
                {
                    Invoke(nameof(DisactivateImmunity), TIME_UNDER_IMMUNITY);
                }
                _isUnderImmunity = value;
            }
        }

        private bool _isUnderImmunity;
        public override int Health
        {
            get
            {
                return _health;
            }
            set
            {
                var clampedhealth = Mathf.Clamp(value, 0, MaxHealth);
                if (_health != clampedhealth)
                    OnHealthChanged?.Invoke(clampedhealth);
                if (clampedhealth == 0)
                    OnDeath?.Invoke();
                _health = clampedhealth;
            }
        }
        private int _health;
        public override int MaxHealth { get => _maxHealth; }
        private int _maxHealth;
        public override int Team { get => _team; }
        private int _team;
        public override int Damage { get => _damage; }
        private int _damage;
        public const float SIT_COEFF = 0.7f;
        public const float DEFAULT_SPEED = 4f;
        public const float TIME_UNDER_IMMUNITY = 2f;
        public const float DEFAULT_JUMP_FORCE = 24f;

        protected override void Awake()
        {
            base.Awake();
            _team = 1;
            _damage = 1;
            _maxHealth = 100;
            SetDirection(Vector2.right);
            CurrentJumpForce = DEFAULT_JUMP_FORCE;
            CurrentSpeed = DEFAULT_SPEED;
        }

        private void DisactivateImmunity()
        {
            _isUnderImmunity = false;
        }
    }
}

