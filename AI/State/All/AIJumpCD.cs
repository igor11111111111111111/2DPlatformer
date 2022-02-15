using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class AIJumpCD : MonoBehaviour
    {
        public bool Passed
        {
            get => _passed;
            set
            {
                if (value == false)
                {
                    _passed = value;
                    Invoke(nameof(SetTrue), _cdTime);
                }
            }
        }
        private bool _passed = true;
        private float _cdTime = 1f;

        private void SetTrue()
        {
            _passed = true;
        }
    }
}
