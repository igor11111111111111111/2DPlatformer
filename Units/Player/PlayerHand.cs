using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class PlayerHand : MonoBehaviour
    { 
        [SerializeField] private Transform _attackColldier;
        protected IUseController _controller { get; set; }

        private void Start()
        {
            _controller = PlayerController.Instance;
            (_controller as PlayerController).OnDirectionXchanged += ChangeDirection;
        }

        private void OnDisable()
        {
            (_controller as PlayerController).OnDirectionXchanged -= ChangeDirection;
        }

        private void ChangeDirection(float sign)
        {
            _attackColldier.transform.localPosition *= -1;
        }
    }
}
