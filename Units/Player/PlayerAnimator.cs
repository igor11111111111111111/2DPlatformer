using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class PlayerAnimator : UnitAnimator
    { 
        private PlayerController _controller => PlayerController.Instance;

        protected override void Start()
        {
            base.Start();

            _controller.OnMove += Move;
            _controller.OnJump += Jump;
            _controller.OnSit += Sit;
            _controller.OnLanding += Landing;
            _controller.OnBlock += Block;
            _controller.OnEnterDoor += EnterDoor;
            _controller.OnGrab += Grab;
            _controller.OnDodge += Dodge;
            (_data as PlayerData).OnRessurect += Ressurect;
            (_data as PlayerData).FistData.OnStartAttack += Attack;
            MainController.Instance.OnLoad += Invisible;
            EscMenuPanel.Instance.OnNewGame += Invisible;
            EscMenuPanel.Instance.OnLastCheckpoint += Invisible;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _controller.OnMove -= Move;
            _controller.OnJump -= Jump;
            _controller.OnSit -= Sit;
            _controller.OnLanding -= Landing;
            _controller.OnBlock -= Block;
            _controller.OnEnterDoor -= EnterDoor;
            _controller.OnGrab -= Grab;
            _controller.OnDodge -= Dodge;
            (_data as PlayerData).OnRessurect -= Ressurect;
            (_data as PlayerData).FistData.OnStartAttack -= Attack;
            MainController.Instance.OnLoad -= Invisible;
            EscMenuPanel.Instance.OnNewGame -= Invisible;
            EscMenuPanel.Instance.OnLastCheckpoint -= Invisible;
        }

        protected override void Move(bool value)
        {
            _animator.SetBool("IsMove", value);
        }

        private void Jump()
        {
            _animator.SetTrigger("OnJump");
        }

        private void Sit(bool value)
        {
            _animator.SetBool("IsSit", value);
        }

        private void Landing()
        {
            //_animator.SetTrigger("OnLanding");
        }

        int index = 0;
        protected override void Attack()
        {
            if (index == 3) index = 0;
           
            _animator.SetTrigger("OnAttack");
            _animator.SetInteger("AttackIndex", index);
            index++;
        }

        private void Block(/*int index*/)
        {
            var index = 1;
            _animator.SetTrigger("OnBlock");
            _animator.SetInteger("BlockIndex", index/*Random.Range(0, 2)*/);
        }

        private void EnterDoor()
        {
            _animator.SetTrigger("OnEnterDoor");
        }

        private void Ressurect()
        {
            _animator.SetTrigger("OnRessurect");
        }

        private void Invisible()
        {
            _animator.SetTrigger("OnInvisible");
        }

        private void Dodge(bool active)
        {
            if(active) 
                _animator.SetTrigger("OnDodge");
        }

        private void Grab(Enums.Grab grab)
        {
            if (grab == Enums.Grab.none)
            {
                _animator.SetBool("IsPull", false);
                _animator.SetBool("IsPush", false);
            }
            else if (grab == Enums.Grab.pull)
            {
                _animator.SetBool("IsPull", true);
                _animator.SetBool("IsPush", false);
            }
            else if (grab == Enums.Grab.push)
            {
                _animator.SetBool("IsPull", false);
                _animator.SetBool("IsPush", true);
            }
        }
    }
}
