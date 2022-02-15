using UnityEngine;

namespace Platformer2D
{ 
    public class BeatsPlayer : MonoBehaviour
    {
        protected int _damage = 1;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.TryGetComponent(out PlayerData playerData);
            if (!playerData) return;

            playerData.OnTakeDamage?.Invoke(_damage);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.TryGetComponent(out PlayerData playerData);
            if (!playerData) return;

            playerData.OnTakeDamage?.Invoke(_damage);
        }
    }
}
