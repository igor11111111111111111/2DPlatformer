using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class TommyGunAnimator : MonoBehaviour
    {
        private PlayerController _controller => PlayerController.Instance;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _controller.OnMove += Move;
        }

        private void OnDisable()
        {
            _controller.OnMove -= Move;
        }

        private void Move(bool value)
        {
            _animator.SetBool("IsMove", value);
        }
    }
}
