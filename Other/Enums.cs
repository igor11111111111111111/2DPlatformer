using UnityEngine;

namespace Platformer2D
{
    public class Enums : MonoBehaviour
    {
        public enum Grab
        {
            none,
            push,
            pull,
            stand
        }

        public enum Gun
        {
            Fist,
            TommyGun,
            Bomb,
        }

        public enum ArenaSide
        {
            Left,
            Right
        }

        public enum BossAttack
        {
            Combo,
            Runup
        }
    }
}
