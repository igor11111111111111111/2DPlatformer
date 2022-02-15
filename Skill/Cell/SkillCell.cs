using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Platformer2D
{ 
    public abstract class SkillCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    { 
        [SerializeField] private Animation _animation;
        public SkillData SkillData => _skillData;
        [SerializeField] private SkillData _skillData;

        private void Awake()
        {
            GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _animation.Play("OnEnterCell");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _animation.Play("OnExitCell");
        }

        private void OnEnable()
        {
            _animation.Play("OnEnableCell");
        }
    }
}
