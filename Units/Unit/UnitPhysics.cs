using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{ 
    public class UnitPhysics : MonoBehaviour
    {
        protected UnitData _unitData;
        protected PolygonCollider2D _polygonCol;
        protected int _lastGroundCollCount;
        protected PhysicsMaterial2D _physicsMat;

        protected virtual void Awake()
        {
            _unitData = GetComponent<UnitData>();
            _polygonCol = GetComponent<PolygonCollider2D>();
            InitPhysicsMaterial();
            _polygonCol.sharedMaterial = _physicsMat;
        }

        protected virtual void Start()
        {
            _unitData.OnDeath += Death;
        }

        protected virtual void OnDisable()
        {
            _unitData.OnDeath -= Death;
        }

        protected virtual void Update()
        {
            var newCount = Mathf.Clamp01(_unitData.GroundColliders.Count);
            if (_lastGroundCollCount != newCount)
            {
                var deltha = _lastGroundCollCount - newCount;
                var friction = deltha > 0 ? 0.001f : 0.4f;
                ChangeFriction(_polygonCol, friction);
            }
            _lastGroundCollCount = _unitData.GroundColliders.Count;
        }

        protected virtual void InitPhysicsMaterial()
        {
            _physicsMat = new PhysicsMaterial2D();
            _physicsMat.bounciness = 0;
            _physicsMat.friction = 0.4f;
        }

        protected void ChangeFriction(Collider2D collider, float value)
        {
            collider.sharedMaterial.friction = value;
            collider.enabled = false;
            collider.enabled = true;
        }

        protected virtual void Death()
        {
            _unitData.Alive = false;
        }
    }
}
