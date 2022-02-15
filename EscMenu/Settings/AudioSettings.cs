using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Platformer2D
{
    public abstract class AudioSettings : MonoBehaviour
    {
        [SerializeField] AudioMixerGroup _mixer;
        protected string _audioName;
        protected bool _active;
        private float _currentVolume;

        public void Active(bool active)
        {
            _active = active;
            SetAudioVolume(active ? Mathf.Lerp(-40, 10, _currentVolume) : -80);
        }

        public void Value(float value)
        {
            _currentVolume = value;
            if (_active)
                SetAudioVolume(Mathf.Lerp(-40, 10, value));
        }

        private void SetAudioVolume(float value)
        {
            _mixer.audioMixer.SetFloat(_audioName, value);
        }
    }
}

