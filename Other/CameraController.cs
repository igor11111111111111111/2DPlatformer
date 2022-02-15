using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _speed = 2.0f;
        [SerializeField] private Transform _player;
        private Transform _currentTarget;
        [SerializeField] private SaveGame _save;
        private Vector3 _offset;

        private void OnEnable()
        {
            _save.OnLoad += () => SetTarget(_player, new Vector3(0, 0, 0));
        }

        private void OnDisable()
        {
            _save.OnLoad -= () => SetTarget(_player, new Vector3(0, 0, 0));
        }

        private void Update()
        {
            Vector3 position = new Vector3(_currentTarget.position.x, _currentTarget.position.y, -10) + _offset;
            transform.position = Vector3.Lerp(transform.position, position, _speed * Time.deltaTime);
        }

        public void SetTarget(Transform target, Vector3 offset)
        {
            _currentTarget = target;
            _offset = offset;
        }

        public void SetTarget(Transform target)
        {
            _currentTarget = target;
        }

        public void SetTarget(Vector3 offset)
        {
            _offset = offset;
        }
    }
}

