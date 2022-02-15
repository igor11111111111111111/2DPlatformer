using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{ 
    public class SkeletMeleeAnimator : UnitAnimator
    {
        private SkeletMeleeController _controller;

        protected override void Awake()
        {
            base.Awake();
            _controller = GetComponent<SkeletMeleeController>();
        }

        protected override void Start()
        {
            base.Start();
            _controller.OnAttack += Attack;
            _controller.OnMove += Move;
            _controller.OnJump += Jump;
            _controller.OnEnterDoor += EnterDoor;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _controller.OnAttack -= Attack;
            _controller.OnMove -= Move;
            _controller.OnJump -= Jump;
            _controller.OnEnterDoor -= EnterDoor;
        }

        protected override void Move(bool value)
        {
            _animator.SetBool("IsMove", value);
        }

        protected override void Attack()
        {
            var index = Random.Range(0, 3);
            _animator.SetInteger("AttackIndex", index);
            _animator.SetTrigger("OnAttack");
        }

        private void Jump()
        {
            _animator.SetTrigger("OnJump");
        }

        private void EnterDoor()
        {
            _animator.SetTrigger("OnEnterDoor");
        }
    }
}
