using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class Arrow : BeatsPlayer
    { 
        private SpriteRenderer _renderer;
        private Vector2 _direction;
        private float _speed = 2f;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            transform.Translate(new Vector3(_direction.x, _direction.y, 0) * _speed * Time.deltaTime);
        }

        public void SetStartSettings(bool flipX, Vector2 direction)
        {
            _renderer.flipX = !flipX; // убрать ! т.к спрайты в разном направлении
            _direction = direction;
        }
    }
}