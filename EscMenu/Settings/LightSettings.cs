using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class LightSettings : MonoBehaviour
    {
        [SerializeField] private GameObject _globalLight;
        [SerializeField] private GameObject _playerLamp;
        [SerializeField] private GameObject _localLight;

        public void Active(bool active)
        {
            _globalLight.SetActive(active);
            _localLight.SetActive(!active);
            _playerLamp.SetActive(!active);
        }
    }
}
