using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class SkeletMeleeHand : MonoBehaviour
    {
        protected Vector3 _startPosition;
        [SerializeField] private AIData _aIData;

        private void Start()
        {
            _startPosition = transform.localPosition;
            _aIData.OnDirectionXchanged += ChangeDirection;
        }

        private void OnDestroy()
        {
            _aIData.OnDirectionXchanged -= ChangeDirection;
        }

        private void ChangeDirection(float sign)
        {
            transform.localPosition = _startPosition * sign;
        }
    }
}
