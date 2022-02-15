using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer2D
{
    public class LivesBar : MonoBehaviour
    {
        [SerializeField] private Text _text;
        private PlayerData _playerData;

        private void OnEnable()
        {
            _playerData = FindObjectOfType<PlayerData>();
            _playerData.OnHealthChanged += Refresh;
        }

        private void OnDisable()
        {
            _playerData.OnHealthChanged -= Refresh;
        }

        public void Refresh(int value)
        {
            _text.text = value.ToString();
        }
    }
}


