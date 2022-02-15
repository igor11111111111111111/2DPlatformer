using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class TommyGun : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private Transform _body;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _bulletParent;
        private PlayerController _controller => PlayerController.Instance;
        private int _gunDirection => SpriteRenderer.flipX ? -1 : 1;
        private Coroutine _customUpdate;

        private void Awake()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            DeInitGunHandler();
        }

        private void Start()
        {
            _controller.OnDirectionXchanged += ChangeDirection;
            _playerData.TommyGunData.OnHoldAttack += Shoot;
            _playerData.TommyGunData.OnInitSkill += InitGunHandler;
            _playerData.TommyGunData.OnDeInitSkill += DeInitGunHandler;
        }

        private void OnDisable()
        {
            _controller.OnDirectionXchanged -= ChangeDirection;
            _playerData.TommyGunData.OnHoldAttack -= Shoot;
            _playerData.TommyGunData.OnInitSkill -= InitGunHandler;
            _playerData.TommyGunData.OnDeInitSkill -= DeInitGunHandler;
        }

        private void InitGunHandler()
        {
            _body.gameObject.SetActive(true);
                _customUpdate = StartCoroutine(CustomUpdate());
        }

        private void DeInitGunHandler()
        {
            _body.gameObject.SetActive(false);
            if(_customUpdate != null)
                StopCoroutine(_customUpdate);
        }

        private IEnumerator CustomUpdate()
        {
            while (true)
            {
                FollowMouse();
                yield return new WaitForSeconds(0.05f);
            }
        }

        private void FollowMouse()
        {
            var mouseDirection = Mouse.NormalizedDirection(_playerData.transform.position).ToVector2XY();
            //if (Mathf.Sign(mouseDirection.x) != _gunDirection)//!
            //{
            //    mouseDirection = new Vector2(_gunDirection, 0);
            //}
            _body.right = _gunDirection * mouseDirection;
        }

        public void ChangeDirection(float sign)
        {
            SpriteRenderer.flipX = !SpriteRenderer.flipX;
            transform.localPosition *= -1;
            transform.localRotation = Quaternion.Euler(-transform.localRotation.eulerAngles);
            _bulletParent.transform.localPosition *= new Vector2(-1, 1);
        }

        private void Shoot()
        {
            Bullet bullet = Instantiate(_bullet);
            bullet.Init(_bulletParent.transform.position, _body.right, _gunDirection, _playerData);
        }
    }
}
