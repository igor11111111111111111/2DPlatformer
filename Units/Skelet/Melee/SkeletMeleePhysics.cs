using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class SkeletMeleePhysics : UnitPhysics
    {
        private SkeletMeleeController _controller;
        private SkeletMeleeData _data => _unitData as SkeletMeleeData;

        protected override void Awake()
        {
            base.Awake();
            _controller = GetComponent<SkeletMeleeController>();
        }

        protected override void Start()
        {
            base.Start();
            _controller.OnMove += Move;
            _controller.OnJump += Jump;
            _data.OnDirectionXchanged += ChangeDirRenderer;
            _data.OnTakeDamage += TakeDamage;
            _data.OnDeath += Death;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _controller.OnMove -= Move;
            _controller.OnJump -= Jump;
            _data.OnDirectionXchanged -= ChangeDirRenderer;
            _data.OnTakeDamage -= TakeDamage;
            _data.OnDeath -= Death;
        }

        private void Move(bool value)
        {
            if (value)
            {
                var moveCoeff = _data.GroundColliders.Count > 0 ?
                UnitData.MOVE_COEFF : UnitData.IN_JUMP_MOVE_COEFF;

                _data.RigidBody.velocity = new Vector2(_data.Direction.x * SkeletMeleeData.DEFAULT_SPEED * moveCoeff, _data.RigidBody.velocity.y);
                //transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right * _data.Direction.x, SkeletMeleeData.DEFAULT_SPEED * Time.deltaTime);
            }
            else
            {
                _data.RigidBody.velocity = new Vector2(0, _data.RigidBody.velocity.y);
            }
        }

        private void Jump()
        {
            _data.RigidBody.AddForce(transform.up * SkeletMeleeData.DEFAULT_JUMP_FORCE, ForceMode2D.Impulse);
        }

        public void AnimatorEventEnterDoor()
        {
            transform.position = _data.CurentDoor.GetExitDoorPosition();
        }

        public void AnimatorEventAttack()
        {
            (_data.AttackCollider as SkeletMeleeAttackCollider).Attack();
        }
         
        private void TakeDamage(int value)
        {
            _data.RigidBody.AddForce(-_data.Direction.normalized * transform.right * 5, ForceMode2D.Impulse);
            _data.Health -= value;
        }

        protected override void Death()
        {
            base.Death();
            gameObject.layer = 19; // only with blocks
            Destroy(GetComponent<MeleeLogic>());
        }

        private void ChangeDirRenderer(float sign)
        {
            _data.SpriteRenderer.flipX = sign > 0 ? false : true;
        }

    }
}
