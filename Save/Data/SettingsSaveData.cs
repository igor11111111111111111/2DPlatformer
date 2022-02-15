using System;
using UnityEngine;
using Newtonsoft.Json;
 
namespace Platformer2D
{
    [Serializable]
    public class SettingsSaveData : ISaveData
    {
        public bool IsAudioEffectActive;
        public float AudioEffectVolume;
        public bool IsMusicActive;
        public float MusicVolume;
        public bool IsFullScreenActive;
        public bool IsLightActive;
    }
}
