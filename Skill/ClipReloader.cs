using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public class ClipReloader : MonoBehaviour
    {
        private PlayerController _controller => PlayerController.Instance;
        public UnityAction OnStartReload;
        public UnityAction OnEndReload;
        private float _reloadTime = 3f;
        public bool IsDuringReload => _isDuringReload;
        private bool _isDuringReload = false;

        private void Start()
        {
            _controller.OnReloadTommyGun += StartReload;
        }

        private void OnDisable()
        {
            _controller.OnReloadTommyGun -= StartReload;
        }

        private void StartReload()
        {
            if (_isDuringReload) return;

            OnStartReload?.Invoke();
            StartCoroutine(EndReload());
        }

        private IEnumerator EndReload()
        {
            _isDuringReload = true;
            yield return new WaitForSeconds(_reloadTime);
            _isDuringReload = false;
            OnEndReload?.Invoke();
        }
    }
}
