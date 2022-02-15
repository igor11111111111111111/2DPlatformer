using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Platformer2D
{
    public class SkillWheelPanel : MonoBehaviour
    {
        public UnityAction OnChangeSkill;
        [SerializeField] private GameObject _body;
        [SerializeField] private SkillWheelAnimator _animator;
        [SerializeField] private RectTransform _transform;
        [SerializeField] private SkillWheel _skillWheel;
        private PlayerController _playerController => PlayerController.Instance;
        private MainController _mainController => MainController.Instance;
        private Coroutine _timedClose;

        private void Awake()
        {
            _body.SetActive(false);
        }

        private void Start()
        {
            _playerController.OnSkillWheelStarted += Open;
            _playerController.OnSkillWheelCanceled += Close;
            _mainController.OnEsc += OnEsc;
        }

        private void OnDisable()
        {
            _playerController.OnSkillWheelStarted -= Open;
            _playerController.OnSkillWheelCanceled -= Close;
            _mainController.OnEsc -= OnEsc;
        }

        private void Open()
        {
            if (_timedClose != null)
                StopCoroutine(_timedClose);
            _transform.anchoredPosition = Mouse.ScreenPosition;
            _body.SetActive(true);
            _animator.Open();
        }
         
        private void Close()
        {
            var skillCell = GetCellWithRay();
            if (skillCell)
            {
                OnChangeSkill?.Invoke();
                _skillWheel.InitData(skillCell.SkillData);
            }
            _animator.Close();
            _timedClose = StartCoroutine(TimedClose());
        }

        private void OnEsc(bool active)
        {
            if (!active) return;

            _body.SetActive(false);
            _animator.Default();
        }

        private SkillCell GetCellWithRay()
        {
            var pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Mouse.ScreenPosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            if (results.Count > 0)
            {
                return results[0].gameObject.GetComponent<SkillCell>();
            }

            return null;
        }

        IEnumerator TimedClose()
        {
            yield return new WaitForSeconds(_animator.GetCloseTime());
            _body.SetActive(false);
        }
    }
}
