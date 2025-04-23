using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UISurvivalGameOver : MonoBehaviour
    {
        [SerializeField] protected Canvas _canvas;
        [Space]
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;

        public Button RestartButton => _restartButton;
        public Button MenuButton => _menuButton;

        public void Show()
        {
            _canvas.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _canvas.gameObject.SetActive(false);
        }
    }
}