using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class UIBomb : MonoBehaviour
    {
        [SerializeField] private GameObject _body;
        private PlayerData _playerData;
        private Vector3 _offset = new Vector3(0, 0.8f, 0);
        private float _timeRefreshPosition = 0.03f;
        private Coroutine _refreshPosition;

        private void Start()
        {
            _playerData = FindObjectOfType<PlayerData>();
            _body.SetActive(false);

            _playerData.BombData.OnStartAttack += () => ActivatePanel(true);
            _playerData.BombData.OnCancelAttack += () => ActivatePanel(false);
        }

        private void OnDisable()
        {
            _playerData.BombData.OnStartAttack -= () => ActivatePanel(true);
            _playerData.BombData.OnCancelAttack -= () => ActivatePanel(false);
        }

        private void ActivatePanel(bool active)
        {
            if(active)
            {
                _refreshPosition = StartCoroutine(RefreshPosition());
            }
            else if(_refreshPosition != null)
            {
                StopCoroutine(_refreshPosition);
            }

            _body.SetActive(active);
        }

        private IEnumerator RefreshPosition()
        {
            while(true)
            {
                _body.transform.position = _playerData.transform.position + _offset;
                yield return new WaitForSeconds(_timeRefreshPosition);
            }
        }
    }
}
