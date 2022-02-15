using UnityEngine;

namespace Platformer2D
{
    public class SettingsJson : Json
    { 
        public SettingsJson()
        {
            _filePath += "/Settings.json";
        }
    }
}
