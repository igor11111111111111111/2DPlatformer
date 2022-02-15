using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class Door : UseOnTriggerText
    { 
        [SerializeField] private Door _exit;
        private Vector3 _offset = new Vector3(0, 0.5f, 0);
        protected override string _text => "F";

        public Vector3 GetExitDoorPosition()
        {
            return _exit.transform.position + _offset;
        }
    }
}
