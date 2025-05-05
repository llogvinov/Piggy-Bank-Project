using System;
using System.Linq;
using YG;

namespace Core.Services.Localization
{
    public class YandexLocalizationService : ILocalizationService
    {
        public event Action<string> LanguageChanged;

        public AllLocalizationData AllLocalizationData { get; private set; }

        public string Language { get; private set; } = "ru";

        public YandexLocalizationService(AllLocalizationData allLocalizationData)
        {
            AllLocalizationData = allLocalizationData;
            YG2.onSwitchLang += InvokeLanguageChanged;
        }

        private void InvokeLanguageChanged(string language)
        {
            LanguageChanged?.Invoke(language);
        }

        public void SwitchLanguage(string language)
        {
            YG2.SwitchLanguage(language);
            Language = language;
        }

        public string GetLocalizedDataById(string id)
        {
            foreach (var partData in AllLocalizationData.PartDataList)
            {
                var data = partData.Data.FirstOrDefault(d => d.Id == id);
                if (data != null)
                {
                    return Language switch
                    {
                        "ru" => data.RU,
                        "en" => data.EN,
                        _ => data.EN
                    };
                }
            }
            return "";
        }

        ~YandexLocalizationService()
        {
            YG2.onSwitchLang -= InvokeLanguageChanged;
        }
    }
}