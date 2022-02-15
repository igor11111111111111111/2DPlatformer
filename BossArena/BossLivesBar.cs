using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer2D
{
    public class BossLivesBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private BullData _data;
        [SerializeField] private GameObject _body;
        [SerializeField] private BossArena _arena;
        
        private void OnEnable()
        {
            _data.OnHealthChanged += Refresh;
            _arena.OnPlayerEnter += OpenPanel;
            _data.OnDeath += ClosePanel;
            _arena.OnSaveLoading += ClosePanel;
        }

        private void OnDisable()
        {
            _data.OnHealthChanged -= Refresh;
            _arena.OnPlayerEnter -= OpenPanel;
            _data.OnDeath -= ClosePanel;
            _arena.OnSaveLoading -= ClosePanel;
        }

        private void Start()
        {
            _slider.maxValue = _data.MaxHealth;
            _body.SetActive(false);
        }

        private void Refresh(int value)
        {
            _slider.value = value;
        }

        private void OpenPanel()
        {
            _body.SetActive(true);
        }

        private void ClosePanel()
        {
            _body.SetActive(false);
        }
    }
}
