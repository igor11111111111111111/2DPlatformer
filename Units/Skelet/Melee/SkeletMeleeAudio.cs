using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class SkeletMeleeAudio : UnitAudio
    { 
        private SkeletMeleeController _controller;
        private SkeletMeleeData _data;
        [SerializeField] private AudioSource _bodyAudio;
        [SerializeField] private AudioSource _mouthAudio;

        [SerializeField] private List<AudioClip> _attacks;
        [SerializeField] private List<AudioClip> _takeDamages;
        [SerializeField] private AudioClip _death;
        [SerializeField] private AudioClip _jump;
        [SerializeField] private AudioClip _move;

        private void Awake()
        {
            _controller = GetComponentInParent<SkeletMeleeController>();
            _data = GetComponentInParent<SkeletMeleeData>();
        }

        private void Start()
        {
            _controller.OnMove += Move;
            _controller.OnAttack += Attack;
            _controller.OnJump += Jump;
            _data.OnTakeDamage += TakeDamage;
            _data.OnDeath += Death;
        }

        private void OnDisable()
        {
            _controller.OnMove -= Move;
            _controller.OnAttack -= Attack;
            _controller.OnJump -= Jump;
            _data.OnTakeDamage -= TakeDamage;
            _data.OnDeath -= Death;
        }

        private void Move(bool active)
        {
            if (active && _bodyAudio.clip != _move && _data.GroundColliders.Count != 0)
            {
                _bodyAudio.clip = _move;
                _bodyAudio.time = Random.Range(0, _bodyAudio.clip.length);
                _bodyAudio.Play();
            }
            else if (!active || _data.GroundColliders.Count == 0)
            {
                _bodyAudio.clip = null;
            }
        }

        private void TakeDamage(int _)
        {
            var index = Random.Range(0, _takeDamages.Count - 1);
            var clip = _takeDamages[index];
            _mouthAudio.PlayOneShot(clip);
        }

        private void Attack()
        {
            var index = Random.Range(0, _attacks.Count - 1);
            var clip = _attacks[index];
            _mouthAudio.PlayOneShot(clip);
        }

        private void Jump()
        {
            _mouthAudio.PlayOneShot(_jump);
        }

        private void Death()
        {
            _mouthAudio.PlayOneShot(_death);
        }
    }
}

