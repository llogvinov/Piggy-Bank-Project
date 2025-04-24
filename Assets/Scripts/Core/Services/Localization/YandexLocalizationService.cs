using System;
using System.Linq;
using UnityEngine;
using YG;

namespace Core.Services.Localization
{
    public class YandexLocalizationService : ILocalizationService
    {
        public event Action<string> LanguageChanged;

        public LocalizationData LocalizationData { get; private set; }

        public string Language { get; private set; } = "ru";

        public YandexLocalizationService(LocalizationData localizationData)
        {
            LocalizationData = localizationData;
            YG2.onSwitchLang += InvokeLanguageChanged;
        }

        private void InvokeLanguageChanged(string language)
        {
            Debug.Log("language changed");
            LanguageChanged?.Invoke(language);
        }

        public void SwitchLanguage(string language)
        {
            Debug.Log("switch language");
            YG2.SwitchLanguage(language);
            Language = language;
        }

        public string GetLocalizedDataById(string id)
        {
            var data = LocalizationData.Data.FirstOrDefault(d => d.Id == id);
            if (data == null)
            {
                Debug.LogError($"Data not found for id {id}");
                return "";
            }
            return Language switch
            {
                "ru" => data.RU,
                "en" => data.EN,
                _ => data.EN
            };
        }

        ~YandexLocalizationService()
        {
            YG2.onSwitchLang -= InvokeLanguageChanged;
        }
    }
}