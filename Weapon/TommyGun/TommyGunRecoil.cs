using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class TommyGunRecoil : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _body;
        [SerializeField] private PlayerData _playerData;
        private int _gunDirection => _spriteRenderer.flipX ? -1 : 1;
        private float _currentRecoil;
        private float _recoilDeltha;
        private float _resistaneDeltha;
        private float _maxRecoil;
        private float _startRecoil;
        private float _delthaToMaxRecoil;
        private Coroutine _resistance;
        private Vector3 _startPosition;
        private float _maxRecoilSpread;

        private void Awake()
        {
            _startPosition = _body.localPosition;

            _recoilDeltha = 0.04f;
            _resistaneDeltha = 0.04f;
            _maxRecoil = 0.3f;
            _startRecoil = 0.05f;
            _delthaToMaxRecoil = 1.3f; // > 1
            _maxRecoilSpread = 0.17f; // < _maxRecoil
        }

        private void Start()
        {   
            _playerData.TommyGunData.OnHoldAttack += () => StartCoroutine(Recoil());
            _playerData.TommyGunData.OnStartAttack += ShootStarted;
            _playerData.TommyGunData.OnCancelAttack += ShootCanceled;
        } 

        private void OnDisable()
        {
            _playerData.TommyGunData.OnHoldAttack -= () => StartCoroutine(Recoil());
            _playerData.TommyGunData.OnStartAttack -= ShootStarted;
            _playerData.TommyGunData.OnCancelAttack -= ShootCanceled;
        }

        private void ShootStarted()
        {
            if (_resistance != null)
                StopCoroutine(_resistance);
        }

        private void ShootCanceled()
        {
            if (_resistance == null)
                _resistance = StartCoroutine(Resistance());
        }

        private IEnumerator Recoil()
        {
            int time = 0;
            while (time < 5)
            {
                _body.localPosition = Vector2.Lerp(_body.localPosition, -_gunDirection * _body.right * 0.2f, _recoilDeltha);
                _body.right += new Vector3(0, _currentRecoil, 0);
                time++;
                yield return new WaitForSeconds(0.01f);
            }

            if (_currentRecoil == 0)
                _currentRecoil = _startRecoil;
            if (Mathf.Sign(_currentRecoil) != _gunDirection)
                _currentRecoil *= -1;
            _currentRecoil *= _delthaToMaxRecoil;
            _currentRecoil = Mathf.Clamp(_currentRecoil, -_maxRecoil, _maxRecoil);
            if (Mathf.Abs(_currentRecoil) == _maxRecoil)
                _currentRecoil -= Random.Range(0, _maxRecoilSpread);
        }

        private IEnumerator Resistance()
        {
            while (_body.localPosition.x != 0 && _body.localPosition.y != 0)
            {
                _body.localPosition = Vector2.MoveTowards(_body.localPosition, _gunDirection * _startPosition, _resistaneDeltha);
                _currentRecoil = 0;
                yield return new WaitForSeconds(0.01f);
            }
            _resistance = null;
        }
    }
}

