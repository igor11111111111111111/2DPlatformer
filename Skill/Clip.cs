using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public class Clip : MonoBehaviour
    {
        [SerializeField] private ClipReloader _reloader;
        public int MaxBulletCount => _maxBulletCount;
        private int _maxBulletCount;
        public int CurrentBulletCount
        {
            get => _currentBulletCount;
            set
            {
                _currentBulletCount = Mathf.Clamp(value, 0, _maxBulletCount);
            }
        }
        private int _currentBulletCount;

        private void Awake()
        {
            _maxBulletCount = 30;
            _currentBulletCount = _maxBulletCount;
        }

        private void OnEnable()
        {
            _reloader.OnEndReload += Reload;
        }

        private void OnDisable()
        {
            _reloader.OnEndReload -= Reload;
        }

        private void Reload()
        {
            _currentBulletCount = _maxBulletCount;
        }
    }
}
