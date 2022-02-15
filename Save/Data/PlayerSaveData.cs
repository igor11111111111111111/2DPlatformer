using System;

namespace Platformer2D
{
    [Serializable]
    public class PlayerSaveData : ISaveData
    {
        public SerializedVector3 Position;
        public int Health;
        public int Bomb;
          
        public PlayerSaveData(SerializedVector3 position, int health, int bomb)
        {
            Position = position;
            Health = health;
            Bomb = bomb;
        }
    }
}

