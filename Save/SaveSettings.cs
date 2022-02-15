using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public class SaveSettings : MonoBehaviour
    {
        [SerializeField] private NewSettings _newSettings;
        [SerializeField] private FullScreenSettings _fullScreen;
        [SerializeField] private MusicSettings _music;
        [SerializeField] private EffectsSettings _effects;
        [SerializeField] private LightSettings _light;

        private void Start()
        {
            SettingsJson _saveSystem = new SettingsJson();
            SettingsSaveData _data = new SettingsSaveData();
            if (_saveSystem.IsExistsFilePath())
            {
                _saveSystem.Load(_data);
            }
            else
            {
                _data = _newSettings.Data;
                _saveSystem.Save(_data);
            }

            Load(_data);
        }

        private void Load(SettingsSaveData data)
        {
            _fullScreen.Active(data.IsFullScreenActive);
            _light.Active(data.IsLightActive);
            _effects.Active(data.IsAudioEffectActive);
            _effects.Value(data.AudioEffectVolume);
            _music.Active(data.IsMusicActive);
            _music.Value(data.MusicVolume);
        }
    }
}
