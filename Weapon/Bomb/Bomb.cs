using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Platformer2D
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]

    public class Bomb : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rigidBody;
        private CapsuleCollider2D _collider;
        private BrokenBomb[] _brokenBombs;
        private int _damage = 5;
        [SerializeField] private GameObject _brokenBombContainer;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CapsuleCollider2D>();
            _brokenBombs = _brokenBombContainer.GetComponentsInChildren<BrokenBomb>();

            Invoke(nameof(Explode), 3f);
        }

        public void AddForce(Vector2 force)
        {
            _rigidBody.AddForce(force, ForceMode2D.Impulse);
        }

        private void Explode()
        {
            //_collider.isTrigger = true;
            _brokenBombContainer.SetActive(true);

            foreach (var brokenBomb in _brokenBombs)
            {
                brokenBomb.BlowUp(_brokenBombContainer.transform);
            }

            var objects = Physics2D.OverlapCircleAll(transform.position, 1);
            foreach (var obj in objects)
            {
                obj.TryGetComponent(out BreakableBlockData block);
                block?.DestroyBlock();
                
                obj.TryGetComponent(out ThinkData findedData);
                findedData?.OnTakeDamage?.Invoke(_damage);
            }

            transform.localRotation = new Quaternion(0, 0, 0, 0);
            _animator.SetTrigger("Explode");
        }

        public void AnimatorEventDestroy()
        {
            Destroy(gameObject);
        }
    }
}

