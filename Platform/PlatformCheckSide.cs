using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class PlatformCheckSide : MonoBehaviour
    {
        private Platform _parent;

        private void Awake()
        {
            _parent = GetComponentInParent<Platform>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out PlayerData playerData))
            {
                _parent.IsTrigger(true);
                Debug.Log(collider.GetType() + " " + "true");
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out PlayerData playerData))
            {
                //_parent.IsTrigger(false);
                Debug.Log(collider.name + " " + "false");
            }
        }
    }
}

