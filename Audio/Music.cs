using UnityEngine;
using UnityEngine.UI;

namespace Platformer2D
{
    public class Music : MonoBehaviour
    {
        [SerializeField] private Button _nextAudio;
        private AudioSource _audio;
        private AudioClip[] _music;
        private int _index;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            _music = Resources.LoadAll<AudioClip>("Music");
            _nextAudio.onClick.AddListener(() => NextClip());
            _index = Random.Range(0, _music.Length);
        }

        private void Update()
        {
            if (!_audio.isPlaying)
                NextClip();
        }

        private void NextClip()
        {
            _audio.clip = _music[_index];
            _audio.Play();
            _index++;
            if (_index == _music.Length)
            {
                _index = 0;
            }
        }
    }
}
