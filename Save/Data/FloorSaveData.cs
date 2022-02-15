using System;

namespace Platformer2D
{
    [Serializable] 
    public class FloorSaveData : ISaveData
    {
        public string Id;
        public SerializedVector3 Position;

        public FloorSaveData(string id, SerializedVector3 position)
        {
            Id = id;
            Position = position;
        }
    }
}