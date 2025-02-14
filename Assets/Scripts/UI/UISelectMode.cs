using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UISelectMode : UIBase
    {
        public static event Action NormalModeSelected, SurvivalModeSelected;

        [SerializeField] private Button _normalModeButton;
        [SerializeField] private Button _survivalModeButton;

        protected override void Start()
        {
            base.Start();

            _normalModeButton.onClick.AddListener(OnNormalModeSelected);
            _survivalModeButton.onClick.AddListener(OnSurvivalModeSelected);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _normalModeButton.onClick.RemoveListener(OnNormalModeSelected);
            _survivalModeButton.onClick.RemoveListener(OnSurvivalModeSelected);
        }

        private void OnNormalModeSelected() => 
            NormalModeSelected?.Invoke();

        private void OnSurvivalModeSelected() => 
            SurvivalModeSelected?.Invoke();
    }
}