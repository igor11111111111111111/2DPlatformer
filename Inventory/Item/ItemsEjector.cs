using UnityEngine;

namespace Platformer2D
{
    public class ItemsEjector : MonoBehaviour
    {
        [SerializeField] private Environment _environment;
        private PlayerData _playerData;

        private void Start()
        {
            _playerData = FindObjectOfType<PlayerData>();
        }
         
        public void Eject(IItem item)
        {
            var prefab = Instantiate(item.Prefab, _environment.transform);
            prefab.transform.position = _playerData.transform.position;
            //var directionX = Mouse.WorldPosition.x > _playerData.transform.position.x ? 1 : -1;
            var direction = Mouse.NormalizedDirection(_playerData.transform.position).x;
            var force = new Vector2(80 * direction, 100);
            prefab
                .AddComponent<MovingAlongCurve>()
                .StartMoving(force)
                .OnStopMoving += () => InitIPickable(item, prefab);
        }

        private void InitIPickable(IItem item, GameObject prefab)
        {
            var data = _environment.Add(item, prefab);
            var pickable = prefab.GetComponent<IPickable>();
            pickable.Id = item.Id;
            pickable.PrefabData = data;
        }
    }
}