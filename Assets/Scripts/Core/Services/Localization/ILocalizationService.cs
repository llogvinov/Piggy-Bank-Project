using System;

namespace Core.Services.Localization
{
    public interface ILocalizationService : IService
    {
        event Action<string> LanguageChanged;
        public AllLocalizationData AllLocalizationData { get; }
        string Language { get; }
        string GetLocalizedDataById(string id);
        void SwitchLanguage(string language);
    }
}