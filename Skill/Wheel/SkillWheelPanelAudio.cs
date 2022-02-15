using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class SkillWheelPanelAudio : MonoBehaviour
    {
        [SerializeField] private SkillWheelPanel _skillPanel;
        [SerializeField] private AudioSource _effectAudio;
        [SerializeField] private AudioClip _changeSkill;

        private void Start()
        {
            _skillPanel.OnChangeSkill += ChangeSkill;
        }

        private void OnDisable()
        {
            _skillPanel.OnChangeSkill -= ChangeSkill;
        }

        private void ChangeSkill()
        {
            _effectAudio.PlayOneShot(_changeSkill);
        }
    }
}
