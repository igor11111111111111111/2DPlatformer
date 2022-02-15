using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D 
{
    [RequireComponent(typeof(Rigidbody2D))]

    public abstract class UnitData : ThinkData
    {
        public UnityAction OnDeath;
        [HideInInspector] public bool Alive = true;
        [HideInInspector] public Door CurentDoor;
        [HideInInspector] public List<Collider2D> GroundColliders;

        public abstract int Team { get; }
        public abstract int Damage { get; }
        public abstract int Health { get; set; }
        public abstract int MaxHealth { get; }
        public Vector2 Direction { get; private set; }
        public UnitAttackCollider AttackCollider { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Rigidbody2D RigidBody { get; private set; }
        public UnitAnimator UnitAnimator { get; private set; }
        //protected int _health;
        public const float MOVE_COEFF = 1f;
        public const float IN_JUMP_MOVE_COEFF = 0.8f;

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        public virtual void SetTargetDirection(ITarget target)
        {
            Direction = (target.DataTransform.position - transform.position).normalized;
        }

        protected virtual void Awake()
        {
            GroundColliders = new List<Collider2D>();
            RigidBody = GetComponent<Rigidbody2D>();
            UnitAnimator = GetComponent<UnitAnimator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            AttackCollider = GetComponentInChildren<UnitAttackCollider>();
        }
    }
}
