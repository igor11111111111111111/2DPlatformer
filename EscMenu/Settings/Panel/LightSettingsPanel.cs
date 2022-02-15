using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer2D
{
    public class LightSettingsPanel : MonoBehaviour
    {
        [SerializeField] private LightSettings _lightSettings;
        private Toggle _toggle;
        public bool IsToggleOn
        {
            get
            {
                return _toggle.isOn;
            }
            set
            {
                _toggle.isOn = value;
            }
        }

        private void Awake()
        {
            _toggle = GetComponentInChildren<Toggle>();
            _toggle.onValueChanged.AddListener(_lightSettings.Active);
        }
    }
}
