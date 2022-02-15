using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class UnitGroundCheck : MonoBehaviour
    {
        protected UnitData _data;

        private void Awake()
        {
            _data = GetComponentInParent<UnitData>();
        }

        public virtual void OnTriggerEnter2D(Collider2D collider)
        {
            _data.GroundColliders.Add(collider);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            _data.GroundColliders.Remove(collider);
        }
    }
}
