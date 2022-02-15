using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Platformer2D
{
    public class FindObstacle : RaycastFind
    {
        private List<RaycastHit2D> _rayCastResults;

        public FindObstacle(StateData stateData) : base(stateData)
        {
            _rayCastResults = new List<RaycastHit2D>();
            _allResults = new List<RaycastHit2D>();
            _filter = _data.Eyes.ObstacleFilter;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (_time % (_delayCustomUpdate + 1) == 0)
            {
                foreach (var line in _data.Eyes.Lines)
                {
                    line.SetPosition(1, line.GetPosition(0));
                }
            }
        }

        public override void LogicUpdate()
        {
            RaycastAll();
            DrawLines();
            if (FoundImpassableObstacle())
            {
                _data.StateMachine.ChangeMainState((_data.Logic as MeleeLogic).Idle);
            }
            else if (_data.JumpCD.Passed)
            {
                if (NeededJump())
                    _data.StateMachine.ChangeMainState((_data.Logic as MeleeLogic).Jump);
            }
        }

        private void RaycastAll()
        {
            _allResults.Clear();
            Raycast(new Vector2(0.15f, -1.45f), 0.45f);
            Raycast(new Vector2(0.15f, -0.85f), 0.45f);
            Raycast(new Vector2(0.35f, -0.2f), 0.45f);
            Raycast(new Vector2(0.35f, 0.45f), 0.45f);
            Raycast(new Vector2(0.35f, 1.05f), 0.45f);
            Raycast(new Vector2(0.35f, 1.65f), 0.45f);
            Raycast(new Vector2(0.35f, 2.25f), 0.45f);
        }

        protected void Raycast(Vector2 offset, float distance)
        {
            Vector2 origin = _data.UnitData.transform.position.ToVector2XY() + 
                new Vector2(Mathf.Sign(_data.UnitData.Direction.x) * offset.x, offset.y);
            Physics2D.Raycast(origin, new Vector2(_data.UnitData.Direction.x, 0), _filter, _rayCastResults, distance);
            if (_rayCastResults.Count > 0)
                _allResults.Add(_rayCastResults[0]);
            else
                _allResults.Add(new RaycastHit2D());
        }

        protected void DrawLines()
        {
            for (int i = 0; i < _data.Eyes.Lines.Count; i++)
            {
                if (_allResults[i].point != Vector2.zero)
                    _data.Eyes.Lines[i].SetPosition(1,
                        _allResults[i].point - _data.UnitData.transform.position.ToVector2XY());
                else
                    _data.Eyes.Lines[i].SetPosition(1,_data.Eyes.Lines[i].GetPosition(0));
            }
        }

        private bool NeededJump()
        {
            if((FoundJumpableObstacle(2) || FoundJumpableObstacle(3) || 
                FoundJumpableObstacle(2, 4) || FoundJumpableObstacle(3, 4) || FoundCliff(0)) && 
                _data.UnitData.GroundColliders.Count > 0)
                return true;
            return false;
            /*Mathf.Abs(_allResults[0].point.x - _data.UnitData.transform.position.x) <= 0.5f*/
        }

        private bool FoundJumpableObstacle(int index)
        {
            return _allResults[index].transform && !_allResults[index + 1].transform && !_allResults[index + 2].transform;
        }

        private bool FoundJumpableObstacle(int index1, int index2)
        {
            return _allResults[index1].transform && _allResults[index2].transform && !_allResults[index2 + 1].transform && !_allResults[index2 + 2].transform;
        }

        private bool FoundCliff(int index)
        {
            return _allResults[index].transform && !_allResults[index + 1].transform;
        }

        private bool FoundImpassableObstacle()
        {
            return _allResults[3].transform && _allResults[5].transform;
        }
    }
}
