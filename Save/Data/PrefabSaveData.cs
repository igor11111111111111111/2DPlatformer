using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    [Serializable]  
    public class PrefabSaveData : ISaveData
    { 
        public int Id;
        public SerializedVector3 Position;
        [NonSerialized]
        public GameObject GameObject;
         
        public PrefabSaveData(int id, SerializedVector3 position)
        {
            Id = id;
            Position = position;
        }

        public PrefabSaveData(int id, SerializedVector3 position, GameObject gameObject)
        {
            Id = id;
            Position = position;
            GameObject = gameObject;
        }

        public void SetPosition()
        {
            Position = GameObject.transform.position.ToSerializedVector();
        }
    }
}
