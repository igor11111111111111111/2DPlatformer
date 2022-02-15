using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class MovablePlatform : MonoBehaviour
    {
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _end;
        [SerializeField] private Transform _body;
        [SerializeField] private float _speed;
        [SerializeField] private BoxCollider2D _upCollider;
        [SerializeField] private ContactFilter2D _filterPassagers;
        private List<Collider2D> _passagers = new List<Collider2D>();
        private Vector3 _targetPos;

        private void Awake()
        {
            ChangeTarget();
        }

        private void ChangeTarget()
        {
            _targetPos = _targetPos == _start.position ? _end.position : _start.position;
        }

        private void Update()
        {
            if(_body.position == _targetPos)
            {
                ChangeTarget();
            }
            _body.position = Vector3.MoveTowards(_body.position, _targetPos, _speed * Time.deltaTime);

            TransportationPassangers();
        } 

        private void TransportationPassangers()
        {
            _upCollider.GetContacts(_filterPassagers, _passagers);
            if (_passagers.Count == 0) return;
            
            foreach (var passager in _passagers)
            {
                passager.transform.position = Vector3.MoveTowards(passager.transform.position,
                _targetPos + passager.transform.position - _body.position, _speed * Time.deltaTime);
            }
        }

    }
}

