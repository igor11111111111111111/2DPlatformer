using UnityEngine;

namespace Platformer2D
{
    public abstract class UseOnTriggerText : MonoBehaviour
    {
        protected abstract string _text {get;}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerData _))
            {
                InfoButtonPanel.Instance.Show(_text);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerData _))
            {
                InfoButtonPanel.Instance.Show(null);
            }
        }
    }
}
