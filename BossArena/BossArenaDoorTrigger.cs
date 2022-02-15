using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public class BossArenaDoorTrigger : MonoBehaviour
    {
        public UnityAction OnPlayerEnter;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerData player))
            {
                OnPlayerEnter?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}
