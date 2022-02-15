using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class SkillWheelAnimator : MonoBehaviour
    {
        [SerializeField] private Animation _animation;
        private float _closeLength;
        private float _closeTime;
        private string _closeWheel => "CloseWheel";
        private string _openWheel => "OpenWheel";
        private string _defaultWheel => "DefaultWheel";

        private void Awake()
        {
            _closeLength = _animation[_closeWheel].length;
        }

        public void Open()
        {
            _animation.Play(_openWheel);
        }

        public void Close()
        {
            _closeTime = _closeLength - _animation[_openWheel].time;
            if (_closeTime == _closeLength)
                _closeTime = 0;
            _animation[_closeWheel].time = _closeTime;
            _animation.Play(_closeWheel);
        }

        public void Default()
        {
            _animation.Play(_defaultWheel);
        }

        public float GetCloseTime()
        {
            return _closeLength - _closeTime;
        }
    }
}
