using UnityEngine;

namespace Platformer2D
{
    public class BombCreator : MonoBehaviour
    {
        [SerializeField] private Bomb _bomb;
        [SerializeField] private Transform _bombParent;
        private PlayerData _playerData;

        private void Start()
        {
            _playerData = FindObjectOfType<PlayerData>();
            _playerData.BombData.OnCancelAttack += Create;
        }

        private void OnDisable()
        {
            _playerData.BombData.OnCancelAttack -= Create;
        }

        private void Create()
        {
            Bomb bomb = Instantiate(_bomb, _playerData.transform.position, Quaternion.identity);
            bomb.transform.SetParent(_bombParent);
            SetStartSettings(bomb);
        }

        private void SetStartSettings(Bomb bomb)
        {
            var force = _playerData.BombData.CurrentForce;
            var direction = Mouse.NormalizedDirection(_playerData.transform.position);
            bomb.AddForce(direction * force);
        }
    }
}
