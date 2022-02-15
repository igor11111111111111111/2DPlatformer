using System;

namespace Platformer2D
{
    [Serializable]
    public class UnitAISaveData : ISaveData
    {
        public string Id;
        public SerializedVector3 Position;
        public int Health;
         
        public UnitAISaveData(string id, SerializedVector3 position, int health)
        {
            Id = id;
            Position = position;
            Health = health;
        }
    } 
}
