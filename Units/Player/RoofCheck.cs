using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class RoofCheck : MonoBehaviour
    {
        private PlayerData _playerData;

        private void Awake()
        {
            _playerData = GetComponentInParent<PlayerData>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out FloorData data))
            {
                _playerData.UnderRoof = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out FloorData data))
            {
                _playerData.UnderRoof = false;
            }
        }
    }
}
