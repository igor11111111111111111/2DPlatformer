using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class LampSwitch : MonoBehaviour
    {
        [SerializeField] private GameObject _left;
        [SerializeField] private GameObject _right;
        private PlayerController _controller => PlayerController.Instance;

        private void Start()
        {
            ChangeDirection(1);
            _controller.OnDirectionXchanged += ChangeDirection;
        }

        private void OnDisable()
        {
            _controller.OnDirectionXchanged -= ChangeDirection;
        }

        private void ChangeDirection(float sign)
        {
            if(sign > 0)
            {
                _left.SetActive(false);
                _right.SetActive(true);
            }
            else
            {
                _left.SetActive(true);
                _right.SetActive(false);
            }
        }
    }
}
