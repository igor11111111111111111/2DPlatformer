using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class ReloadIndicator : MonoBehaviour
    {
        [SerializeField] private Animation _animation;
        [SerializeField] private ClipReloader _clipReloader;
        [SerializeField] private GameObject _body;

        private void Awake()
        {
            StopReloadHandler();
        }

        private void OnEnable()
        {
            _clipReloader.OnStartReload += StartReloadHandler;
            _clipReloader.OnEndReload += StopReloadHandler;
        }

        private void OnDisable()
        {
            _clipReloader.OnStartReload -= StartReloadHandler;
            _clipReloader.OnEndReload -= StopReloadHandler;
        }

        private void StartReloadHandler()
        {
            _body.SetActive(true);
            _animation.Play();
        }

        private void StopReloadHandler()
        {
            _body.SetActive(false);
            _animation.Stop();
        }
    }
}
