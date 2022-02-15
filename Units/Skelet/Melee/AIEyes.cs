using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class AIEyes : MonoBehaviour
    {
        public ContactFilter2D UnitFilter => _unitFilter;
        [SerializeField] private ContactFilter2D _unitFilter;
        public ContactFilter2D ObstacleFilter => _obstacleFilter;
        [SerializeField] private ContactFilter2D _obstacleFilter;
        public List<LineRenderer> Lines => _lines;
        [SerializeField] private List<LineRenderer> _lines;
        public CircleCollider2D VisionCollider;
    }
}
