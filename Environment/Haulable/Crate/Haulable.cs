using UnityEngine;

namespace Platformer2D
{
    public abstract class Haulable : MonoBehaviour
    {
        protected Rigidbody2D _rigidBody;

        protected virtual void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        public void Sub()
        {
            PlayerController.Instance.OnGrab += Grab;
            PlayerController.Instance.OnGrabStarted += GrabStarted;
        }

        public void UnSub()
        {
            _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);

            PlayerController.Instance.OnGrab -= Grab;
            PlayerController.Instance.OnGrabStarted -= GrabStarted;
        }

        protected abstract void GrabStarted();
        protected abstract void Grab(Enums.Grab grab);
    }

}

