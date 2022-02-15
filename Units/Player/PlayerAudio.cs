using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class PlayerAudio : UnitAudio
    {
        private PlayerController _controller => PlayerController.Instance;
        private PlayerData _data;
        [SerializeField] private AudioSource _bodyAudio;
        [SerializeField] private AudioSource _mouthAudio;
        [SerializeField] private Treatment _treatment;

        [SerializeField] private List<AudioClip> _attacks;
        [SerializeField] private List<AudioClip> _takeDamages;
        [SerializeField] private AudioClip _death;
        [SerializeField] private AudioClip _move;
        [SerializeField] private AudioClip _jump;
        [SerializeField] private AudioClip _resurrect;
        [SerializeField] private AudioClip _pull;
        [SerializeField] private AudioClip _push;
        [SerializeField] private AudioClip _landing;
        [SerializeField] private AudioClip _sit;
        [SerializeField] private AudioClip _healing;

        private void Awake()
        {
            _data = GetComponentInParent<PlayerData>();
        }

        private void Start()
        {
            _controller.OnMove += Move;
            _controller.OnJump += Jump;
            _controller.OnSit += Sit;
            _controller.OnGrab += Grab;
            _data.FistData.OnStartAttack += Attack;
            _controller.OnLanding += Landing;
            _data.OnTakeDamage += TakeDamage;
            _data.OnRessurect += Resurrect;
            _treatment.OnHealing += Healing;
            _data.OnDeath += Death;
        }

        private void OnDisable()
        {
            _controller.OnMove -= Move;
            _controller.OnJump -= Jump;
            _controller.OnSit -= Sit;
            _controller.OnGrab -= Grab;
            _data.FistData.OnStartAttack -= Attack;
            _controller.OnLanding -= Landing;
            _data.OnTakeDamage -= TakeDamage;
            _data.OnRessurect -= Resurrect;
            _treatment.OnHealing -= Healing;
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

        private void Jump()
        {
            _bodyAudio.PlayOneShot(_jump);
        }

        private void Sit(bool active)
        {
            if (active)
            {
                _bodyAudio.PlayOneShot(_sit);
            }
        }

        private void Grab(Enums.Grab grab)
        {
            if (grab == Enums.Grab.pull || grab == Enums.Grab.push)
            {
                _mouthAudio.clip = _pull;
                _mouthAudio.time = Random.Range(0, _mouthAudio.clip.length);
                _mouthAudio.Play();
            }
            else
            {
                _mouthAudio.clip = null;
            }
        }

        private void TakeDamage(int _)
        {
            var index = Random.Range(0, _takeDamages.Count - 1);
            var clip = _takeDamages[index];
            _mouthAudio.PlayOneShot(clip);
        }

        private void Resurrect()
        {
            _bodyAudio.PlayOneShot(_resurrect);
        }

        private void Attack()
        {
            var index = Random.Range(0, _attacks.Count - 1);
            var clip = _attacks[index];
            _mouthAudio.PlayOneShot(clip);
        }

        private void Healing()
        {
            _mouthAudio.PlayOneShot(_healing);
        }

        private void Landing()
        {
            _bodyAudio.PlayOneShot(_landing);
        }

        private void Death()
        {
            _mouthAudio.PlayOneShot(_death);
        }
    }
}
