using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class Bullet : MonoBehaviour
    {
        private float _speed = 10.0f;
        private int _damage = 1;
        private SpriteRenderer _sprite;
        private UnitData _unitData;
        [SerializeField] private Rigidbody2D _rigidbody;
        //[SerializeField] private BoxCollider2D _collider;

        private void Awake()
        {
            _sprite = GetComponentInChildren<SpriteRenderer>();
            Destroy(gameObject, 2f);
        }

        public void Init(Vector3 position, Vector3 transformRight, int direction, UnitData unitData)
        {
            transform.position = position;
            transform.right = transformRight;
            _rigidbody.velocity = direction * transformRight * _speed;
            _unitData = unitData;
            //new Vector3(0, Random.Range(-spread, spread), 0); // AI

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.TryGetComponent(out ThinkData findedData);
            if (findedData && findedData != _unitData)
            {
                findedData.OnTakeDamage?.Invoke(_damage);
            }
            else if(findedData == _unitData)
            {
                return;
            }
            Destroy(gameObject);
        }
    }
}
