using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer2D
{
    public class InfoButtonPanel : MonoBehaviour
    {
        public static InfoButtonPanel Instance;
        [SerializeField] private Text _text;

        private void OnEnable()
        {
            Instance = this;
        }

        private void OnDisable()
        {
            Instance = null;
        }

        private void Awake()
        {
            Show(null);
        }

        public void Show(string text)
        {
            if (text != null)
            {
                _text.text = text;
            }
            else 
            {
                _text.text = null;
            }
        }
    }
}
