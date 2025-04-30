using System.Collections;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UISurvivalGameOver : MonoBehaviour
    {
        [SerializeField] protected Canvas _canvas;
        [Space]
        [SerializeField] private GameObject _revivePanel;
        [SerializeField] private GameObject _buttonsPanel;
        [Space]
        [SerializeField] private Button _reviveButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;

        public Button ReviveButton => _reviveButton;
        public Button RestartButton => _restartButton;
        public Button MenuButton => _menuButton;

        public void Show(bool showRevive)
        {
            SwitchPanels(showRevive);            
            _canvas.gameObject.SetActive(true);
            if (showRevive)
            {
                StartCoroutine(RevivePanelCoroutine());
            }
        }

        public void Hide()
        {
            _canvas.gameObject.SetActive(false);
        }

        private void SwitchPanels(bool showRevive)
        {
            _revivePanel.SetActive(showRevive);
            _buttonsPanel.SetActive(!showRevive);
        }

        private IEnumerator RevivePanelCoroutine()
        {
            yield return new WaitForSeconds(GameConstants.REVIVE_PANEL_DURATION);
            SwitchPanels(false);
        }
    }
}