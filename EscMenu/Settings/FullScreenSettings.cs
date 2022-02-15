using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class FullScreenSettings : MonoBehaviour
    {
        public void Active(bool active)
        {
            Screen.fullScreen = active;
        }
    }
}

