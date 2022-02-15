using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Platformer2D
{
    public class FindTarget : RaycastFind
    {
        private UnitData _targetData;
        private Collider2D _targetCollider;

        public FindTarget(StateData stateData) : base(stateData)
        {
            _filter = _data.Eyes.UnitFilter;
        }

        public override void Enter()
        {
            base.Enter();
            _delayCustomUpdate = 30;

            _data.Target = null;
        }

        public override void CustomUpdate()
        {
            if (TryFindTargetData())
                if (TryRaycast())
                {
                    _data.Target = new UnitTarget(_targetData, _targetCollider);
                    _data.StateMachine.ChangeMainState((_data.Logic as MeleeLogic).MoveToTarget);
                }
        }
        
        private UnitData TryFindTargetData()
        {
            _targetData = null;
            List<Collider2D> results = new List<Collider2D>();
            _data.Eyes.VisionCollider.OverlapCollider(_filter, results);

            _targetCollider = results.Where(col =>
            {
                var findedData = col.transform.GetComponent<UnitData>();
                if (findedData?.Alive == true &&
                    findedData?.Team != _data.UnitData.Team)
                {
                    _targetData = findedData;
                    return true;
                }
                return false;
            })
           .FirstOrDefault();

            return _targetData;
        }

        private bool TryRaycast()
        {
            List<RaycastHit2D> hitsUp = new List<RaycastHit2D>();
            List<RaycastHit2D> hitsDown = new List<RaycastHit2D>();
            Vector2 direction = _targetData.transform.position - _data.UnitData.transform.position;
            Vector2 origin = _data.UnitData.transform.position.ToVector2XY() + new Vector2(0, 0.1f);
            Physics2D.Raycast(origin, direction + new Vector2(0, 0.35f), _data.Eyes.ObstacleFilter, hitsUp, direction.magnitude);
            Physics2D.Raycast(origin, direction + new Vector2(0, -0.55f), _data.Eyes.ObstacleFilter, hitsDown, direction.magnitude);

            Debug.DrawRay(origin, direction + new Vector2(0, 0.35f), Color.yellow, 0.1f);
            Debug.DrawRay(origin, direction + new Vector2(0, -0.55f), Color.yellow, 0.1f);

            return hitsUp.Count == 0 || hitsDown.Count == 0;
        }
    }
}
