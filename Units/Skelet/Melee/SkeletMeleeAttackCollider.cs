using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class SkeletMeleeAttackCollider : UnitAttackCollider
    {
        public void Attack()
        {
            List<Collider2D> results = new List<Collider2D>();
            _collider.OverlapCollider(AttackFilter, results);
            foreach (var result in results)
            {
                result.TryGetComponent(out UnitData findedData);
                if (findedData && _data != findedData)
                {
                    findedData.OnTakeDamage?.Invoke(_data.Damage);
                }
            }
        }
    }
}
