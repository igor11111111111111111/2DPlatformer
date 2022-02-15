using UnityEngine;
using System.Linq;
using UnityEngine.Events;

namespace Platformer2D
{
    public class Treatment : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private PlayerData _playerData;
        public UnityAction OnHealing;
        private IItem _healhPotion;

        private void Start()
        {
            PlayerController.Instance.OnPressHealingButton += PressButtonHandler;
            OnHealing += Healing;
        }

        private void OnDisable()
        {
            PlayerController.Instance.OnPressHealingButton -= PressButtonHandler;
            OnHealing -= Healing;
        }

        private void PressButtonHandler()
        {
            _healhPotion = TryGetHealthPotion();
            if (_healhPotion != null && _playerData.Health != _playerData.MaxHealth)
                OnHealing?.Invoke();
        }

        private IItem TryGetHealthPotion()
        {
            return _inventory.GetItems().Where(i => i.Id == 0).FirstOrDefault();
        }

        private void Healing()
        {
            _inventory.Remove(_healhPotion);
            _playerData.Health++;
        }
    }
}
