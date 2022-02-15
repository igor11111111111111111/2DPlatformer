using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class PlayerAttackCollider : UnitAttackCollider
    {
        [SerializeField] private ContactFilter2D _haulableFilter;

        public void Attack()
        {
            List<Collider2D> results = new List<Collider2D>();
            _collider.OverlapCollider(AttackFilter, results);
            foreach (var result in results)
            {
                //result.TryGetComponent(out BreakableBlockData block);
                //if (block)
                //{
                //    block?.DestroyBlock();
                //    _collider.isTrigger = false;
                //}
                result.TryGetComponent(out ThinkData findedData);
                if (findedData && _data != findedData)
                {
                    findedData.OnTakeDamage?.Invoke(_data.Damage);
                }
            }

            Invoke(nameof(TriggerCD), 0.3f);
        }

        public Haulable TryGrab()
        {
            Collider2D[] results = new Collider2D[1];
            _collider.OverlapCollider(_haulableFilter, results);
            if (results[0])
            {
                results[0].TryGetComponent(out Haulable haulable);
                return haulable;
            }
            return null;
        }

        private void TriggerCD()
        {
            _collider.isTrigger = true;
        }
    }
}
