using UnityEngine;

namespace Platformer2D
{
    public class PlayerGroundCheck : UnitGroundCheck
    {
        public override void OnTriggerEnter2D(Collider2D collider)
        {
            base.OnTriggerEnter2D(collider);

            if (_data.RigidBody.velocity.y <= 0)
            {
                PlayerController.Instance.OnLanding?.Invoke();
            }
        }
    }
}
