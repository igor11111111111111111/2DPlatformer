using System;
using UnityEngine;

namespace Platformer2D
{
    public class AIContactCheck
    {
        private StateData _stateData;
        //private Vector3 targetPos => (_stateData.Target as UnitTarget).UnitData.transform.position;
        private Transform _targetTransform;
        private Vector3 unitPos => _stateData.UnitData.transform.position;

        public AIContactCheck(StateData stateData)
        {
            _stateData = stateData;
        }

        public Vector2 GetDistance(Transform target)
        {
            _targetTransform = target;
            return new Vector2(_targetTransform.position.x - unitPos.x, 
                _targetTransform.position.y - unitPos.y);
        }
    }
}
