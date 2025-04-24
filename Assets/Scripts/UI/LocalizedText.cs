using Core;
using Core.Services.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _id;

        private Text _text;

        private ILocalizationService _localizationService;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _localizationService = AllServices.Container.Single<ILocalizationService>();
        }

        private void Start()
        {
            UpdateText();
            _localizationService.LanguageChanged += OnLanguageChanged;
        }

        private void OnDestroy()
        {
            _localizationService.LanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged(string obj)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            _text.text = _localizationService.GetLocalizedDataById(_id);
        }
    }
}