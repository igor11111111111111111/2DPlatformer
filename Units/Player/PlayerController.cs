using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public class PlayerController : MonoBehaviour, IUseController
    { 
        public static PlayerController Instance;
         
        public UnityAction<Enums.Grab> OnGrab;
        public UnityAction<float> OnDirectionXchanged;
        public UnityAction<bool> OnMove;
        public UnityAction<bool> OnSit; 
        public UnityAction<bool> OnDodge;
        public UnityAction OnLanding; 
        public UnityAction OnJump;
        public UnityAction OnDown;
        public UnityAction OnStartAttack;
        public UnityAction OnHoldAttack;
        public UnityAction OnCancelAttack;
        public UnityAction OnBlock;
        public UnityAction OnEnterDoor;
        public UnityAction OnGrabStarted;
        public UnityAction OnPickUp;
        public UnityAction OnPressHealingButton;
        public UnityAction OnSkillWheelStarted;
        public UnityAction OnSkillWheelCanceled; 
        public UnityAction OnReloadTommyGun;

        [SerializeField] private ItemsInjector _itemsInjector;
        [SerializeField] private BullController _bullController;
        private PlayerData _playerData;
        private CustomInput _input;
        private CustomInput.PlayerActions _actions => _input.Player;

        private Haulable _haulable;
        private Enums.Grab _grab;

        private float _oldDirectionX = 1;

        private bool _isSitHandler
        {
            get
            {
                return _isSit;
            }
            set
            {
                if (_isSit == value) return;

                _isSit = value;
                OnSit?.Invoke(value);
            }
        }

        private bool _isSit;

        private bool _jumpCDPassed = true; // вынести в jumpCD скрипт и void
        private float _jumpCDTime = 0.5f; //

        private bool _isUseDodge;

        private void Awake()
        {
            _input = new CustomInput();

            _playerData = FindObjectOfType<PlayerData>();
            _playerData.OnFreezed += FreezeInput;
            _playerData.OnFreezed += (_) => OnGrab?.Invoke(Enums.Grab.none);
            _playerData.OnDeath += () => FreezeInput(true);
            _playerData.OnDeath += () => OnGrab?.Invoke(Enums.Grab.none);
            _bullController.OnDodge += (active) => _isUseDodge = active;
            OnDodge += (_) => _isUseDodge = false;
        }

        private void Start()
        {
            MainController.Instance.OnEsc += FreezeInput;
            MainController.Instance.OnLoad += () => FreezeInput(true);
            MainController.Instance.OnInventory += FreezeInput;
        }

        private void OnEnable()
        {
            Instance = this;
            _input.Enable();
            OnDirectionXchanged += RefreshGrabDirection;
            OnGrab += initGrab;
            OnMove += GrabOnMove;

            _actions.Sit.started += _ =>
            {
                OnGrab?.Invoke(Enums.Grab.none);
                _isSitHandler = true;
            };
            _actions.Sit.canceled += _ =>
            {
                if (_playerData.UnderRoof) return;
                _isSitHandler = false;
            };

            _actions.Attack.started += _ =>
            {
                if (_isSitHandler) return;
                OnGrab?.Invoke(Enums.Grab.none);
                OnStartAttack?.Invoke();
            };

            _actions.Attack.canceled += _ =>
            {
                OnCancelAttack?.Invoke();
            };

            _actions.EnterDoor.started += _ =>
            {
                var door = PlayerEnvironmentCheck.Instance.TryGet<Door>();
                if (!door) return;
                _playerData.CurentDoor = door;
                OnGrab?.Invoke(Enums.Grab.none);
                OnEnterDoor?.Invoke();
                _playerData.OnFreezed?.Invoke(true);
            };

            _actions.Jump.started += _ =>
            {
                if (_isUseDodge)
                {
                    OnDodge?.Invoke(true);
                }
                else
                {
                    if (_playerData.GroundColliders.Count <= 0 || !_jumpCDPassed) return;
                    OnGrab?.Invoke(Enums.Grab.none);
                    _jumpCDPassed = false;
                    Invoke(nameof(JumpCD), _jumpCDTime);
                    OnJump?.Invoke();
                }
            };

            _actions.Move.started += context =>
            {
                var newDirectionX = context.ReadValue<float>();
                if (_oldDirectionX == newDirectionX) return;
                OnDirectionXchanged?.Invoke(newDirectionX);
                _oldDirectionX = newDirectionX;
            };
            _actions.Move.canceled += _ =>
            {
                OnMove?.Invoke(false);
            };

            _actions.Down.started += _ =>
            {
                OnGrab?.Invoke(Enums.Grab.none);
                OnDown?.Invoke();
            };

            _actions.Grab.started += _ =>
            {
                if (_playerData.GroundColliders.Count == 0 || _isSitHandler) return;
                if (_grab == Enums.Grab.none)
                {
                    var haulable = (_playerData.AttackCollider as PlayerAttackCollider).TryGrab();
                    if (haulable && 
                    Mathf.Abs(_playerData.transform.position.y - haulable.transform.position.y) < 0.1f)
                    {
                        _haulable = haulable;
                        OnGrab?.Invoke(Enums.Grab.stand);
                        OnGrabStarted?.Invoke();
                    }
                }
                else
                {
                    OnGrab?.Invoke(Enums.Grab.none);
                }
            };

            _actions.PickUp.started += _ =>
            {
                var item = PlayerEnvironmentCheck.Instance.TryGet<PickableObject>();
                if (item == null) return;
                _itemsInjector.Inject(item);
            };

            _actions.UseHealthPotion.started += _ =>
            {
                OnPressHealingButton?.Invoke();
            };

            _actions.SkillWheel.started += _ =>
            {
                OnSkillWheelStarted?.Invoke();
            };
            _actions.SkillWheel.canceled += _ =>
            {
                OnSkillWheelCanceled?.Invoke();
            };

            _actions.ReloadTommyGun.started += _ =>
            {
                OnReloadTommyGun?.Invoke();
            };
        }

        private void OnDisable()
        {
            _input.Disable();

            OnDirectionXchanged -= RefreshGrabDirection;
            OnGrab -= initGrab;
            _playerData.OnFreezed -= FreezeInput;
            _playerData.OnFreezed -= (_) => OnGrab?.Invoke(Enums.Grab.none);
            _playerData.OnDeath -= () => FreezeInput(true);
            _playerData.OnDeath -= () => OnGrab?.Invoke(Enums.Grab.none);
            _bullController.OnDodge -= (active) => _isUseDodge = active;
            OnDodge -= (_) => _isUseDodge = false;
            MainController.Instance.OnEsc -= FreezeInput;
            MainController.Instance.OnLoad -= () => FreezeInput(true);
            MainController.Instance.OnInventory -= FreezeInput;
        }

        private void Update()
        {
            if (_isSitHandler && !_playerData.UnderRoof && _actions.Sit.ReadValue<float>() == 0)
            {
                _isSitHandler = false;
            }

            if (_actions.Move.ReadValue<float>() != 0)
            {
                var x = _input.Player.Move.ReadValue<float>();
                _playerData.SetDirection(new Vector2(x, 0));
                OnMove?.Invoke(true);
            }

            if (_actions.Attack.ReadValue<float>() == 1)
            {
                OnHoldAttack?.Invoke();
            };
        }

        private void FreezeInput(bool value)
        {
            if (value)
            {
                _input.Disable();
            }
            else
            {
                _input.Enable();
            }
        }

        private void JumpCD()
        {
            _jumpCDPassed = true;
        }

        private void RefreshGrabDirection(float direction)
        {
            if (!_haulable || _grab == Enums.Grab.none) return;

            Enums.Grab grab;
            if (_haulable.transform.position.x - _playerData.transform.position.x > 0)
            {
                if (direction >= 0)
                {
                    grab = Enums.Grab.push;
                }
                else
                {
                    grab = Enums.Grab.pull;
                }
            }
            else
            {
                if (direction >= 0)
                {
                    grab = Enums.Grab.pull;
                }
                else
                {
                    grab = Enums.Grab.push;
                }
            }

            OnGrab?.Invoke(grab);
        } // вынести

        private void initGrab(Enums.Grab grab)
        {
            if (_grab != grab && grab == Enums.Grab.none)
            {
                _haulable.UnSub();
                _haulable = null;
            }
            else if (_grab == Enums.Grab.none && grab != Enums.Grab.none)
            {
                _haulable.Sub();
            }
            _grab = grab;
        }//

        private void GrabOnMove(bool active)
        {
            if(!active && _grab != Enums.Grab.none)
            {
                OnGrab?.Invoke(Enums.Grab.stand);
            }
            else if (active && _grab == Enums.Grab.stand)
            {
                RefreshGrabDirection(_oldDirectionX);
            }
        }//
    }
}

