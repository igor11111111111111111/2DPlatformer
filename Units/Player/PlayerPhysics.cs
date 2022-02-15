using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Events;

namespace Platformer2D
{
    public class PlayerPhysics : UnitPhysics
    {
        private PlayerController _controller => PlayerController.Instance;
        private PlayerData _data => _unitData as PlayerData;
        private BoxCollider2D _boxCol;
        //private bool _isSit;

        protected override void Start()
        {
            base.Start();
            _controller.OnMove += Move;
            _controller.OnJump += Jump;
            _controller.OnSit += Sit;
            _controller.OnDown += Down;
            _controller.OnDirectionXchanged += ChangeDirRenderer;
            _controller.OnGrab += Grab;
            _controller.OnDodge += Dodge;
            _data.OnTakeDamage += TakeDamage;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _controller.OnMove -= Move;
            _controller.OnJump -= Jump;
            _controller.OnSit -= Sit;
            _controller.OnDown -= Down;
            _controller.OnDirectionXchanged -= ChangeDirRenderer;
            _controller.OnGrab -= Grab;
            _controller.OnDodge -= Dodge;
            _data.OnTakeDamage -= TakeDamage;
            _data.OnDeath -= Death;
        }

        protected override void Awake()
        {
            base.Awake();

            _boxCol = GetComponent<BoxCollider2D>();
            _boxCol.sharedMaterial = _physicsMat;
        }

        protected override void Update()
        {
            var newCount = Mathf.Clamp01(_unitData.GroundColliders.Count);
            if (_lastGroundCollCount != newCount)
            {
                var deltha = _lastGroundCollCount - newCount;
                var friction = deltha > 0 ? 0.001f : 0.4f;
                ChangeFriction(_boxCol, friction);
                ChangeFriction(_polygonCol, friction);
            }
            _lastGroundCollCount = _unitData.GroundColliders.Count;
        }

        private void Move(bool value)
        {
            if (value)
            {
                var moveCoeff = _data.GroundColliders.Count > 0 ?
                UnitData.MOVE_COEFF : UnitData.IN_JUMP_MOVE_COEFF;
                _data.RigidBody.velocity = new Vector2(_data.Direction.x * _data.CurrentSpeed * moveCoeff, _data.RigidBody.velocity.y);
            }
            else
            {
                _data.RigidBody.velocity = new Vector2(0, _data.RigidBody.velocity.y);
            }
        }

        private void Jump()
        {
            _data.RigidBody.AddForce(transform.up * _data.CurrentJumpForce, ForceMode2D.Impulse);
        }

        private void Sit(bool value)
        {
            _data.CurrentSpeed = PlayerData.DEFAULT_SPEED * (value ? PlayerData.SIT_COEFF : 1);
            _data.CurrentJumpForce = PlayerData.DEFAULT_JUMP_FORCE * (value ? PlayerData.SIT_COEFF : 1);
        }

        private void Down()
        {
            if (_data.GroundColliders.Count > 0)
            {
                foreach (var collider in _data.GroundColliders)
                {
                    collider.TryGetComponent(out Platform platform);
                    platform?.IsTrigger(true);
                }
            }
        }

        public void AnimatorEventEnterDoor()
        {
            transform.position = _data.CurentDoor.GetExitDoorPosition();
        }

        public void AnimatorEventAttack()
        {
            (_data.AttackCollider as PlayerAttackCollider).Attack();
        }

        public void AnimatorEventUnfreezeControl()
        {
            _data.OnFreezed?.Invoke(false);
        }

        public void AnimatorEventRessurect()
        {
            _controller.OnGrab?.Invoke(Enums.Grab.none);
            _data.OnRessurect?.Invoke();
        }

        private void TakeDamage(int value)
        {
            if (_data.IsUnderImmunity) return;
            _data.RigidBody.AddForce(transform.up * 7, ForceMode2D.Impulse);
            _data.Health -= value; 
            //if (_data.Health == 0) return;
            _data.IsUnderImmunity = true;
        }

        private void Grab(Enums.Grab grab)
        {
            if (grab != Enums.Grab.none)
            {
                _data.CurrentSpeed = PlayerData.DEFAULT_SPEED / 2;
            }
            else
            {
                _data.CurrentSpeed = PlayerData.DEFAULT_SPEED; // мб баг со скоростью при сидении у ящика
            }
        }

        private void Dodge(bool active)
        {
            _data.IsUnderImmunity = active;
        }

        private void ChangeDirRenderer(float sign)
        {
            _data.SpriteRenderer.flipX = sign > 0 ? false : true;
        }

        //private void Death()
        //{
        //    _controller.OnGrab?.Invoke(Enums.Grab.none);
        //}
    }
}

