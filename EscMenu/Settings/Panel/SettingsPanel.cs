using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private FullScreenSettingsPanel _fullScreenPanel;
        [SerializeField] private LightSettingsPanel _lightPanel;
        [SerializeField] private AudioSettingsPanel _effectsPanel;
        [SerializeField] private AudioSettingsPanel _musicPanel;
        private SettingsSaveData _data;
        private SettingsJson _saveSystem;

        private void Start()
        {
            _data = new SettingsSaveData();
            _saveSystem = new SettingsJson();
            _saveSystem.Load(_data);
            Load();
        }

        private void OnDisable()
        {
            _data.IsFullScreenActive = _fullScreenPanel.IsToggleOn;
            _data.IsLightActive = _lightPanel.IsToggleOn;
            _data.IsAudioEffectActive = _effectsPanel.IsToggleOn;
            _data.AudioEffectVolume = _effectsPanel.SliderValue;
            _data.IsMusicActive = _musicPanel.IsToggleOn;
            _data.MusicVolume = _musicPanel.SliderValue;

            _saveSystem.Save(_data);
        }

        private void Load()
        {
            _fullScreenPanel.IsToggleOn = _data.IsFullScreenActive;
            _lightPanel.IsToggleOn = _data.IsLightActive;
            _effectsPanel.IsToggleOn = _data.IsAudioEffectActive;
            _effectsPanel.SliderValue = _data.AudioEffectVolume;
            _musicPanel.IsToggleOn = _data.IsMusicActive;
            _musicPanel.SliderValue = _data.MusicVolume;
        }
    }
}
