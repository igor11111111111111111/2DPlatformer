using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer2D
{
    public class Light : MonoBehaviour
    {
        [SerializeField] private GameObject _dirLight;
        [SerializeField] private List<GameObject> _torchs;
        [SerializeField] private GameObject _lamp;

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                _torchs.Add(child.gameObject);
            }
        }

        private void Activate()
        {
            _dirLight.SetActive(!_dirLight.activeInHierarchy);
            foreach (var torch in _torchs)
            {
                torch.SetActive(!torch.activeInHierarchy);
            }
            _lamp.SetActive(!_lamp.activeInHierarchy);
        }
    }
}
