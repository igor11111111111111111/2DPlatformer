using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Platformer2D
{
    public class EscMenuPanel : MonoBehaviour
    {
        public static EscMenuPanel Instance;
        public bool IsActiveBody => _body.activeInHierarchy;
        public UnityAction OnNewGame;
        public UnityAction OnLastCheckpoint;
        [SerializeField] private Button _newGame;
        [SerializeField] private Button _lastCheckpoint;
        [SerializeField] private GameObject _body;

        private void Awake()
        {
            _newGame.onClick.AddListener(() => OnNewGame?.Invoke());
            _lastCheckpoint.onClick.AddListener(() => OnLastCheckpoint?.Invoke());
            SetBodyActive(false);
        }

        private void Start()
        {
            MainController.Instance.OnEsc += SetBodyActive;
        }

        private void OnEnable()
        {
            Instance = this;
        }

        private void OnDisable()
        {
            MainController.Instance.OnEsc -= SetBodyActive;
        }

        public void SetBodyActive(bool active)
        {
            _body.SetActive(active);
        }
    }
}
