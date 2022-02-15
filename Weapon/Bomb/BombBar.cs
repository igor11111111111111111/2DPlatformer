using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer2D
{
    public class BombBar : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private GameObject _body;

        private void Start()
        {
            DeInitSkill();
        }

        private void OnEnable()
        {
            _playerData.BombData.OnChangeCount += Refresh;
            _playerData.BombData.OnInitSkill += InitSkill;
            _playerData.BombData.OnDeInitSkill += DeInitSkill;
        }

        private void OnDisable()
        {
            _playerData.BombData.OnChangeCount -= Refresh;
            _playerData.BombData.OnInitSkill -= InitSkill;
            _playerData.BombData.OnDeInitSkill -= DeInitSkill;
        }
         
        public void Refresh(int value)
        {
            _text.fontSize = value > 9 ? 15 : 20;
            _text.text = "x" + value;
        }

        private void InitSkill()
        {
            _body.SetActive(true);
        }

        private void DeInitSkill()
        {
            _body.SetActive(false);
        }
    }
}
