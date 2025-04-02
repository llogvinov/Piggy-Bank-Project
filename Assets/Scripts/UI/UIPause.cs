using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIPause : UIBase
    {
        [SerializeField] private Button _menuButton;

        public Button MenuButton => _menuButton;
        
        protected override void Start()
        {
            base.Start();
            _openButton.onClick.AddListener(PauseGame);
            _closeButton.onClick.AddListener(ResumeGame);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _openButton.onClick.RemoveListener(PauseGame);
            _closeButton.onClick.RemoveListener(ResumeGame);
        }

        private void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
        }
    }
}