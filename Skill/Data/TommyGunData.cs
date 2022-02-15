using UnityEngine;

namespace Platformer2D
{
    public class TommyGunData : SkillData
    {
        [SerializeField] private Clip _clip;
        [SerializeField] private ClipReloader _reloader;
        private bool _shootCDPassed = true;
        private float _shootCDTime = 0.15f;

        protected override void OnEnable()
        {
            base.OnEnable();
            OnHoldAttack += HoldAttackHandler;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            OnHoldAttack -= HoldAttackHandler;
        }

        private void HoldAttackHandler()
        {
            _clip.CurrentBulletCount--;
            _shootCDPassed = false;
            Invoke(nameof(ShootCD), _shootCDTime);
        }

        private void ShootCD()
        {
            _shootCDPassed = true;
        }

        protected override bool StartAttackCondition()
        {
            return true;
        }

        protected override bool HoldAttackCondition()
        {
            return _shootCDPassed && _clip.CurrentBulletCount > 0 && !_reloader.IsDuringReload;
        }

        protected override bool CancelAttackCondition()
        {
            return true;
        }
    }
}
