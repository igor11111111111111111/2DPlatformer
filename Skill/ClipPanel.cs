using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer2D
{
    public class ClipPanel : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private Text _text;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private Clip _clip;
        [SerializeField] private ClipReloader _reloader;

        private void Awake()
        {
            DeInitGunHandler();
        }

        private void Start()
        {
            _reloader.OnEndReload += RefreshText;
            _playerData.TommyGunData.OnHoldAttack += RefreshText;
            _playerData.TommyGunData.OnInitSkill += InitGunHandler;
            _playerData.TommyGunData.OnDeInitSkill += DeInitGunHandler;
        }

        private void OnDisable()
        {
            _reloader.OnEndReload -= RefreshText;
            _playerData.TommyGunData.OnHoldAttack -= RefreshText;
            _playerData.TommyGunData.OnInitSkill -= InitGunHandler;
            _playerData.TommyGunData.OnDeInitSkill -= DeInitGunHandler;
        }

        private void RefreshText()
        {
            _text.text = _clip.CurrentBulletCount + " / " + _clip.MaxBulletCount;
        }

        private void InitGunHandler()
        {
            RefreshText();
            _body.gameObject.SetActive(true);
        }

        private void DeInitGunHandler()
        {
            _body.gameObject.SetActive(false);
        }
    }
}
