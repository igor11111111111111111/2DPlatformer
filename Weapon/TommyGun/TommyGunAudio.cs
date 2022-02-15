using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class TommyGunAudio : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private ClipReloader _reloader;
        [SerializeField] private AudioSource _effectAudio;
        [SerializeField] private AudioClip _attack;
        [SerializeField] private AudioClip _reload;
        private float _minBulletPitch = 0.85f, _maxBulletPitch = 1.15f, _reloadPitch = 1f;

        private void Start()
        {
            _playerData.TommyGunData.OnHoldAttack += Attack;
            _reloader.OnStartReload += Reload;
        }

        private void OnDisable()
        {
            _playerData.TommyGunData.OnHoldAttack -= Attack;
            _reloader.OnStartReload -= Reload;
        }

        private void Attack()
        {
            _effectAudio.pitch = Random.Range(_minBulletPitch, _maxBulletPitch);
            _effectAudio.PlayOneShot(_attack);
        }

        private void Reload()
        {
            _effectAudio.pitch = _reloadPitch;
            _effectAudio.PlayOneShot(_reload);
        }
    }
}

