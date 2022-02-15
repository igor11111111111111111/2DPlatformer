using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class BombSettings
    {
        public readonly Vector2 Force;
        public readonly Vector2 Position;
         
        public BombSettings(Vector2 force, Vector2 position)
        {
            Force = force;
            Position = position;
        }
    }
}

