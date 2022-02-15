using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class DartTrap : Trap, IActivatableTrap
    {
        private bool _attackCDPassed = true;
        private float _attackCDTime = 0.5f;
        private Vector2 shootDirection = new Vector2(10, 0);
        private Arrow _spike;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _spike = Resources.Load<Arrow>("Arrow");
            _renderer = GetComponent<SpriteRenderer>();
            if(_renderer.flipX)
            {
                shootDirection *= -1;
            }
        }

        public void Activate()
        {
            if(_attackCDPassed)
            {
                _attackCDPassed = false;
                Invoke(nameof(AttackCD), _attackCDTime);
                var spike = Instantiate(_spike, transform);
                spike.SetStartSettings(_renderer.flipX, shootDirection);
            }
        }

        private void AttackCD()
        {
            _attackCDPassed = true;
        }
    }
}

