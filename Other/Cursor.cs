using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class Cursor : MonoBehaviour
    {
        [SerializeField] private Texture2D _shoot;
        [SerializeField] private TommyGunData _gunData;
        private MainController _mainController => MainController.Instance;

        private void OnEnable()
        {
            _gunData.OnInitSkill += () => Set(_shoot);
            _gunData.OnDeInitSkill += () => Set(null);
            _mainController.OnEsc += (_) => Set(null);
        }

        private void OnDisable()
        {
            _gunData.OnInitSkill -= () => Set(_shoot);
            _gunData.OnDeInitSkill -= () => Set(null);
            _mainController.OnEsc -= (_) => Set(null);
        }

        private void Set(Texture2D texture)
        {
            UnityEngine.Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        }
    }
}
