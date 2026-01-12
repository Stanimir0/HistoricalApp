using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HistoricalApp.Helpers
{
    public class TranslationService : INotifyPropertyChanged
    {
        private static TranslationService? _instance;
        public static TranslationService Instance => _instance ??= new TranslationService();

        private string _currentLanguage = "en";

        public event PropertyChangedEventHandler? PropertyChanged;

        // Translation properties
        public string YourProfile => LocalizationStrings.Get("YourProfile", _currentLanguage);
        public string EditProfile => LocalizationStrings.Get("EditProfile", _currentLanguage);
        public string UserName => LocalizationStrings.Get("UserName", _currentLanguage);
        public string Rank => LocalizationStrings.Get("Rank", _currentLanguage);
        public string TotalPoints => LocalizationStrings.Get("TotalPoints", _currentLanguage);
        public string AdminPanel => LocalizationStrings.Get("AdminPanel", _currentLanguage);
        public string Logout => LocalizationStrings.Get("Logout", _currentLanguage);
        public string Home => LocalizationStrings.Get("Home", _currentLanguage);
        public string Profile => LocalizationStrings.Get("Profile", _currentLanguage);
        public string Leaderboard => LocalizationStrings.Get("Leaderboard", _currentLanguage);

        public void SetLanguage(string languageCode)
        {
            _currentLanguage = languageCode;
            OnPropertyChanged(string.Empty); // Notify all properties changed
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
