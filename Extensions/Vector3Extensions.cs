using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public static class Vector3Extensions
    {
        public static Vector2 ToVector2XZ(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }

        public static Vector2 ToVector2XY(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.y);
        }

        public static Vector2 ToVector2YZ(this Vector3 vector3)
        {
            return new Vector2(vector3.y, vector3.z);
        }

        public static Vector3 ToVector3(this SerializedVector3 serializedVector3)
        {
            if (serializedVector3 == null)
                return Vector3.zero;
            return new Vector3(serializedVector3.x, serializedVector3.y, serializedVector3.z);
        }

        public static SerializedVector3 ToSerializedVector(this Vector3 vector3)
        {
            return new SerializedVector3(vector3);
        }
    }
}

