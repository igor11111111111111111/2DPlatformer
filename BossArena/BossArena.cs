using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{ 
    public class BossArena : MonoBehaviour
    {
        public UnityAction OnPlayerEnter;
        public UnityAction OnSaveLoading;
        public UnityAction OnBossDeath;
        [SerializeField] private BossArenaDoorTrigger _trigger;
        [SerializeField] private SaveGame _saveGame;
        [SerializeField] private BullData _bullData;
        public float RightPoint => 21.52f;
        public float LeftPoint => 9.25f;
        public float CenterPoint => (LeftPoint + RightPoint) / 2;

        private void OnEnable()
        {
            _trigger.OnPlayerEnter += () => OnPlayerEnter?.Invoke();
            _saveGame.OnLoad += SaveLoadHandler;
            _bullData.OnDeath += () => OnBossDeath?.Invoke();
        }

        private void OnDisable()
        {
            _trigger.OnPlayerEnter -= () => OnPlayerEnter?.Invoke();
            _saveGame.OnLoad -= SaveLoadHandler;
            _bullData.OnDeath -= () => OnBossDeath?.Invoke();
        }

        private void SaveLoadHandler()
        {
            if (_bullData.Alive)
                OnSaveLoading?.Invoke();
            else
                OnBossDeath?.Invoke();
        }
    }
}
