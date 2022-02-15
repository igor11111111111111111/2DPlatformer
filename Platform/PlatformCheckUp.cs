using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class PlatformCheckUp : MonoBehaviour
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
                if (playerData.transform.position.y > transform.position.y)
                {
                    _parent.IsTrigger(false);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out PlayerData playerData))
            {
                _parent.IsTrigger(true);
            }
        }
    }
}

