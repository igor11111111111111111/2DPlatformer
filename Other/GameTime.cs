using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class GameTime : MonoBehaviour
    {
        private void Start()
        {
            MainController.Instance.OnInventory += StopTime;
            MainController.Instance.OnEsc += StopTime;
        }

        private void OnDisable()
        {
            MainController.Instance.OnInventory -= StopTime;
            MainController.Instance.OnEsc -= StopTime;
        }

        private void StopTime(bool active)
        {
            Time.timeScale = active ? 0 : 1;
        }
    }
}
