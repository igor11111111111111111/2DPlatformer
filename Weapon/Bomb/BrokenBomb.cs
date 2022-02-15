using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    
    public class BrokenBomb : MonoBehaviour
    {
        private PolygonCollider2D _collider;
        private Rigidbody2D _rigidBody;
        private float _blowForce = 10000f;

        private void Awake()
        {
            _collider = GetComponent<PolygonCollider2D>();
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        public void BlowUp(Transform parent)
        {
            var vector = transform.position - parent.position;
            _rigidBody.AddForce(vector * _blowForce, ForceMode2D.Impulse);
            transform.SetParent(parent.parent.parent); // Environment
        }
    }
}
