using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class SpikeTrap : MonoBehaviour
    {
        [SerializeField] [Range(1, 4)] private int _height;
        [SerializeField] private LineRenderer _lineRods;
        [SerializeField] private Transform _spike;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _cooldown = 1f;
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private Vector3 _targetPosition;

        private void Awake()
        {
            _startPosition = _spike.localPosition;
            _targetPosition = _endPosition = new Vector3(0, -0.64f * _height, 0);

            StartCoroutine(CustomUpdate());
        }

        private void Update()
        {
            _spike.localPosition = Vector3.MoveTowards(_spike.localPosition, _targetPosition, _speed * Time.deltaTime);

            float linePos = (0.5f - 1.17f * _spike.localPosition.y) / 0.64f;
            _lineRods.SetPosition(1, new Vector3(0, linePos, 0));
            float lineTransformPos = (0.614f - 0.918f * linePos) / 1.17f;
            _lineRods.transform.localPosition = new Vector3(0, lineTransformPos, 0);
        }

        private IEnumerator CustomUpdate()
        {
            while (true)
            {
                if (_spike.localPosition == _endPosition)
                {
                    _targetPosition = _startPosition;
                }
                else if (_spike.localPosition == _startPosition)
                {
                    _targetPosition = _endPosition;
                }

                yield return new WaitForSeconds(_cooldown);
            }
        }
    }
}
