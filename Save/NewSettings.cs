using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class NewSettings : MonoBehaviour
    { 
        public SettingsSaveData Data { get; private set; }
         
        private void Awake()
        {
            Data = new SettingsSaveData();
            Data.IsAudioEffectActive = true;
            Data.AudioEffectVolume = 0.5f;
            Data.IsMusicActive = true;
            Data.MusicVolume = 0.3f;
            Data.IsFullScreenActive = true;
            Data.IsLightActive = true;
        }
    }
}
