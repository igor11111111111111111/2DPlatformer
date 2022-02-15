using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class Crate : Haulable
    {
        private Transform _startParent;
        private Coroutine _customUpdate;
        private PlayerData _playerData;

        protected override void Awake()
        {
            base.Awake();

            _playerData = FindObjectOfType<PlayerData>();
            _startParent = transform.parent;
        }

        protected override void GrabStarted()
        {
            transform.SetParent(_playerData.transform);
            _customUpdate = StartCoroutine(CheckFall(_playerData));
        }

        protected override void Grab(Enums.Grab grab)
        {
            if(grab == Enums.Grab.none && _customUpdate != null)
            {
                transform.SetParent(_startParent);
                StopCoroutine(_customUpdate);
                _rigidBody.velocity = Vector3.zero;
            }
        }

        private IEnumerator CheckFall(PlayerData playerData)
        {
            while (true)
            {
                _rigidBody.velocity = _playerData.RigidBody.velocity;

                if (Mathf.Abs(playerData.transform.position.y - transform.position.y) >= 0.1f)
                {
                    PlayerController.Instance.OnGrab?.Invoke(Enums.Grab.none);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

