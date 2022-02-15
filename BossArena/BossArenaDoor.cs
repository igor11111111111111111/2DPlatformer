using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class BossArenaDoor : MonoBehaviour
    {
        [SerializeField] private GameObject _body;
        [SerializeField] private BossArenaDoorTrigger _trigger;
        [SerializeField] private BossArena _arena;

        private void Awake()
        {
            _body.SetActive(false);
        }

        private void OnEnable()
        {
            _trigger.OnPlayerEnter += CloseDoor;
            _arena.OnSaveLoading += OpenDoor;
            _arena.OnBossDeath += OpenDoorWithoutTrigger;
        }

        private void OnDisable()
        {
            _trigger.OnPlayerEnter -= CloseDoor;
            _arena.OnSaveLoading -= OpenDoor;
            _arena.OnBossDeath -= OpenDoorWithoutTrigger;
        }

        private void CloseDoor()
        {
            _body.SetActive(true);
        }

        private void OpenDoor()
        {
            _body.SetActive(false);
            _trigger.gameObject.SetActive(true);
        }

        private void OpenDoorWithoutTrigger()
        {
            _body.SetActive(false);
            _trigger.gameObject.SetActive(false);
        }
    }
}
