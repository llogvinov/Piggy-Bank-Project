using Core;
using Core.Services.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private bool _shouldUpdateUI = true;

        private Text _text;

        private ILocalizationService _localizationService;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _localizationService = AllServices.Container.Single<ILocalizationService>();
        }

        private void Start()
        {
            if (_shouldUpdateUI)
            {
                UpdateText();
            }
            _localizationService.LanguageChanged += OnLanguageChanged;
        }

        private void OnDestroy()
        {
            _localizationService.LanguageChanged -= OnLanguageChanged;
        }

        public string GetValue()
        {
            Debug.Log(_localizationService.GetLocalizedDataById(_id));
            return _localizationService.GetLocalizedDataById(_id);
        }

        private void OnLanguageChanged(string lang)
        {
            if (_shouldUpdateUI)
            {
                UpdateText();
            }
        }

        private void UpdateText()
        {
            _text.text = _localizationService.GetLocalizedDataById(_id);
        }
    }
}