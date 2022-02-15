using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Platformer2D
{
    class InventoryCell : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        public UnityAction OnEject;

        [SerializeField] private Text _name;
        [SerializeField] private Image _icon;
        private Transform _draggingParent, _originalParent;

        private void Awake()
        {
            _originalParent = transform.parent;
        }

        public void Init(Transform draggingParent)
        {
            _draggingParent = draggingParent;
        }

        public void Render(IItem item)
        {
            _name.text = item.Name;
            _icon.sprite = item.Sprite;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.parent = _draggingParent;
        } 

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Mouse.WorldPosition.ToVector2XY(); /*new Vector3(position.x, position.y, 0);*/
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (In((RectTransform)_originalParent))
                InsertInGrid();
            else
                OnEject?.Invoke();
        }

        private void InsertInGrid()
        {
            int closestIndex = 0;

            for (int i = 0; i < _originalParent.transform.childCount; i++)
            {
                if (Vector3.Distance(transform.position, _originalParent.GetChild(i).position) <
                   Vector3.Distance(transform.position, _originalParent.GetChild(closestIndex).position))
                {
                    closestIndex = i;
                }
            }

            transform.parent = _originalParent;
            transform.SetSiblingIndex(closestIndex);
        }

        private bool In(RectTransform originalParent)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(originalParent, transform.position);
        }
    }
}
