using UnityEngine;

namespace Platformer2D
{
    public class Mouse : MonoBehaviour
    { 
        public static Vector2 ScreenPosition => _screenPosition;
        private static Vector2 _screenPosition;
        public static Vector3 WorldPosition => _worldPosition;
        private static Vector3 _worldPosition;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            _screenPosition = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
            _worldPosition = _camera.ScreenToWorldPoint(_screenPosition);
        }

        public static Vector2 NormalizedDirection(Vector2 start)
        {
            var mouseDirection = _worldPosition.ToVector2XY() - start;
            return mouseDirection.normalized;
        }

        public static Vector3 NormalizedDirection(Vector3 start)
        {
            var mouseDirection = _worldPosition - start;
            return mouseDirection.normalized;
        }
    }
}
