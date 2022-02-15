using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class ItemsInjector : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Environment _environment;

        public void Inject(PickableObject pickableObject)
        {
            _environment.Remove(pickableObject);
            _inventory.Add(pickableObject.Id);
            Destroy(pickableObject.gameObject);
        }
    }
}
