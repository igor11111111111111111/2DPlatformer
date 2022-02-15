using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer2D
{ 
    public class AudioSettingsPanel : MonoBehaviour
    {
        [SerializeField] private AudioSettings _settings;
        private Toggle _toggle;
        private Slider _slider;
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
        public float SliderValue
        {
            get
            {
                return _slider.value;
            }
            set
            {
                _slider.value = value;
            }
        }

        private void Awake()
        {
            _toggle = GetComponentInChildren<Toggle>();
            _slider = GetComponentInChildren<Slider>();
            _toggle.onValueChanged.AddListener(_settings.Active);
            _slider.onValueChanged.AddListener(_settings.Value);
        }
    }
}
