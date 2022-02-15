using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{ 
    public class UnitAttackCollider : MonoBehaviour
    {
        [SerializeField] public ContactFilter2D AttackFilter;
        protected Collider2D _collider;
        protected UnitData _data;

        protected virtual void Start()
        {
            _data = GetComponentInParent<UnitData>();
            _collider = GetComponent<Collider2D>();
        }
          
    }
}

