using System.Globalization;
using Microsoft.Maui.Storage;

namespace HistoricalApp.Helpers
{
    public static class LocalizationHelper
    {
        private const string LanguagePreferenceKey = "AppLanguage";

        public static void SetLanguage(string languageCode)
        {
            var culture = new CultureInfo(languageCode);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            
            // Save preference
            Preferences.Set(LanguagePreferenceKey, languageCode);
        }

        public static string GetCurrentLanguage()
        {
            return Preferences.Get(LanguagePreferenceKey, "en");
        }

        public static void InitializeLanguage()
        {
            var savedLanguage = GetCurrentLanguage();
            SetLanguage(savedLanguage);
        }
    }
}
