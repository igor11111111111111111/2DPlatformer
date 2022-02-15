using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public class MovingAlongCurve : MonoBehaviour
    { 
        public UnityAction OnStopMoving;
        private Rigidbody2D _rigidBody;
        private Collider2D _collider;
         
        public MovingAlongCurve StartMoving(Vector2 force)
        {
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = false;
            _rigidBody = gameObject.AddComponent<Rigidbody2D>();
            _rigidBody.freezeRotation = true;
            _rigidBody.AddForce(force);
            gameObject.layer = 19; // onlyWithBlocks
            return this;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 11 && AcceptableDistance(collision)) // ground
            {
                gameObject.layer = 13; // itemtrigger
                _collider.isTrigger = true;
                OnStopMoving?.Invoke();
                Destroy(_rigidBody);
                Destroy(this);
            }
        }

        private bool AcceptableDistance(Collision2D collision)
        {
            var distance = (transform.position.ToVector2XY() - collision.collider.ClosestPoint(transform.position));

            return (distance.x == 0) ||
                   (distance.x >= -9.4 && distance.x <= -9.6) ||
                   (Mathf.Abs(distance.x) >= 0 && Mathf.Abs(distance.x) <= 0.1) && 
                   distance.y > 0;
        }
    }
}