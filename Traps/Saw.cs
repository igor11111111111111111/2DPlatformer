using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class Saw : BeatsPlayer
    {
        [SerializeField] private HingeJoint2D _ropeJoint;
        private float _ropeSpeed = 200f;
        private const float _cd = 1.5f;
        private JointMotor2D _motor;

        private void Awake()
        {
            _motor = _ropeJoint.motor;
            _motor.motorSpeed = _ropeSpeed;
            _ropeJoint.motor = _motor;

            StartCoroutine(CustomUpdate());
        }

        private IEnumerator CustomUpdate()
        {
            while(true)
            {
                yield return new WaitForSeconds(_cd);
                _motor.motorSpeed *= -1;
                _ropeJoint.motor = _motor;
            }
        }
    }

}
