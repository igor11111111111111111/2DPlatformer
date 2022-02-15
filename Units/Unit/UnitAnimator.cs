using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
      
    abstract public class UnitAnimator : MonoBehaviour
    {
        protected Animator _animator;
        protected UnitData _data;

        protected virtual void Awake()
        {
            _data = GetComponent<UnitData>();
            _animator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            _data.OnTakeDamage += TakeDamage;
            _data.OnDeath += Death;
        }

        protected virtual void OnDisable()
        {
            _data.OnTakeDamage -= TakeDamage;
            _data.OnDeath -= Death;
        }

        protected virtual void TakeDamage(int value)
        {
            _animator.SetTrigger("OnTakeDamage");
        }

        protected virtual void Death()
        {
            _animator.SetTrigger("OnDeath");
        }

        protected abstract void Move(bool value);
        protected abstract void Attack();
    }
}

