using System.Collections;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UISurvivalGameOver : UIBase
    {
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
            Show();
            if (showRevive)
            {
                StartCoroutine(RevivePanelCoroutine());
            }
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