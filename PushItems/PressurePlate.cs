using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class PressurePlate : MonoBehaviour
    {
        [SerializeField] private Trap _trap;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out PlayerData playerData))
            {
                (_trap as IActivatableTrap).Activate();
            }
        }
    }
}
