using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UISelectMode : UIBase
    {
        public static event Action NormalModeSelected, SurvivalModeSelected;

        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;
        [Space]
        [SerializeField] private Button _normalModeButton;
        [SerializeField] private Button _survivalModeButton;

        private void Start()
        {
            _openButton.onClick.AddListener(Show);
            _closeButton.onClick.AddListener(Hide);

            _normalModeButton.onClick.AddListener(OnNormalModeSelected);
            _survivalModeButton.onClick.AddListener(OnSurvivalModeSelected);
        }

        private void OnDestroy()
        {
            _openButton.onClick.RemoveListener(Show);
            _closeButton.onClick.RemoveListener(Hide);

            _normalModeButton.onClick.RemoveListener(OnNormalModeSelected);
            _survivalModeButton.onClick.RemoveListener(OnSurvivalModeSelected);
        }

        private void OnNormalModeSelected() => 
            NormalModeSelected?.Invoke();

        private void OnSurvivalModeSelected() => 
            SurvivalModeSelected?.Invoke();
    }
}