using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public class SkillWheel : MonoBehaviour
    {
        private PlayerController _controller => PlayerController.Instance;

        private UnityAction _startAction;
        private UnityAction _holdAction;
        private UnityAction _cancelAction;

        [SerializeField] private SkillData _defaultSkill;
        private SkillData _oldData;

        private void Awake()
        {
            InitData(_defaultSkill);
        }

        private void Start()
        {
            _controller.OnStartAttack += () => _startAction?.Invoke();
            _controller.OnHoldAttack += () => _holdAction?.Invoke();
            _controller.OnCancelAttack += () => _cancelAction?.Invoke();
        }

        private void OnDisable()
        {
            _controller.OnStartAttack -= () => _startAction?.Invoke();
            _controller.OnHoldAttack -= () => _holdAction?.Invoke();
            _controller.OnCancelAttack -= () => _cancelAction?.Invoke();
        }

        public void InitData(SkillData data)
        {
            if(_oldData != null)
                _oldData.OnDeInitSkill?.Invoke();
            data.InitActions(ref _startAction, ref _holdAction, ref _cancelAction);
            data.OnInitSkill?.Invoke();

            _oldData = data;
        }
    }
}
