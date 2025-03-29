using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIGameOver : MonoBehaviour
    {
        [SerializeField] protected Canvas _canvas;
        [Space]
        [SerializeField] private Button _restartButton;
        [SerializeField] protected Button _menuButton;

        public Button RestartButton => _restartButton;
        public Button MenuButton => _menuButton;

        public virtual void Show()
        {
            _canvas.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            _canvas.gameObject.SetActive(false);
        }
    }
}