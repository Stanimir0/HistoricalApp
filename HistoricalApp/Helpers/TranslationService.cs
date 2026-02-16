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

        // Common
        public string Back => LocalizationStrings.Get("Back", _currentLanguage);
        
        // Profile Page
        public string YourProfile => LocalizationStrings.Get("YourProfile", _currentLanguage);
        public string EditProfile => LocalizationStrings.Get("EditProfile", _currentLanguage);
        public string UserName => LocalizationStrings.Get("UserName", _currentLanguage);
        public string Rank => LocalizationStrings.Get("Rank", _currentLanguage);
        public string TotalPoints => LocalizationStrings.Get("TotalPoints", _currentLanguage);
        public string AdminPanel => LocalizationStrings.Get("AdminPanel", _currentLanguage);
        public string Logout => LocalizationStrings.Get("Logout", _currentLanguage);
        
        // Bottom Navigation
        public string Home => LocalizationStrings.Get("Home", _currentLanguage);
        public string Profile => LocalizationStrings.Get("Profile", _currentLanguage);
        public string Leaderboard => LocalizationStrings.Get("Leaderboard", _currentLanguage);
        public string Ranking => LocalizationStrings.Get("Ranking", _currentLanguage);
        
        // Home Page
        public string WelcomeBack => LocalizationStrings.Get("WelcomeBack", _currentLanguage);
        public string Explorer => LocalizationStrings.Get("Explorer", _currentLanguage);
        public string Battles => LocalizationStrings.Get("Battles", _currentLanguage);
        public string Events => LocalizationStrings.Get("Events", _currentLanguage);
        public string People => LocalizationStrings.Get("People", _currentLanguage);
        public string Top10 => LocalizationStrings.Get("Top10", _currentLanguage);
        public string Shop => LocalizationStrings.Get("Shop", _currentLanguage);
        
        // Leaderboard Page
        public string Points => LocalizationStrings.Get("Points", _currentLanguage);
        public string Daily => LocalizationStrings.Get("Daily", _currentLanguage);
        public string Weekly => LocalizationStrings.Get("Weekly", _currentLanguage);
        public string Monthly => LocalizationStrings.Get("Monthly", _currentLanguage);
        public string Reward => LocalizationStrings.Get("Reward", _currentLanguage);
        public string YourRank => LocalizationStrings.Get("YourRank", _currentLanguage);
        
        // Shop Page
        public string Currency => LocalizationStrings.Get("Currency", _currentLanguage);
        public string Coins => LocalizationStrings.Get("Coins", _currentLanguage);
        public string Purchase => LocalizationStrings.Get("Purchase", _currentLanguage);
        public string Purchased => LocalizationStrings.Get("Purchased", _currentLanguage);
        public string InsufficientFunds => LocalizationStrings.Get("InsufficientFunds", _currentLanguage);
        
        // Login Page
        public string HistoricalApp => LocalizationStrings.Get("HistoricalApp", _currentLanguage);
        public string WelcomeBackTitle => LocalizationStrings.Get("WelcomeBackTitle", _currentLanguage);
        public string Email => LocalizationStrings.Get("Email", _currentLanguage);
        public string EmailPlaceholder => LocalizationStrings.Get("EmailPlaceholder", _currentLanguage);
        public string Password => LocalizationStrings.Get("Password", _currentLanguage);
        public string PasswordPlaceholder => LocalizationStrings.Get("PasswordPlaceholder", _currentLanguage);
        public string Login => LocalizationStrings.Get("Login", _currentLanguage);
        public string DontHaveAccount => LocalizationStrings.Get("DontHaveAccount", _currentLanguage);
        public string CreateAccount => LocalizationStrings.Get("CreateAccount", _currentLanguage);
        
        // Register Page
        public string JoinUs => LocalizationStrings.Get("JoinUs", _currentLanguage);
        public string CreateYourAccount => LocalizationStrings.Get("CreateYourAccount", _currentLanguage);
        public string ConfirmPassword => LocalizationStrings.Get("ConfirmPassword", _currentLanguage);
        public string ConfirmPasswordPlaceholder => LocalizationStrings.Get("ConfirmPasswordPlaceholder", _currentLanguage);
        public string ChoosePasswordPlaceholder => LocalizationStrings.Get("ChoosePasswordPlaceholder", _currentLanguage);
        public string Register => LocalizationStrings.Get("Register", _currentLanguage);
        public string BackToLogin => LocalizationStrings.Get("BackToLogin", _currentLanguage);
        
        // Quiz Selection Page
        public string ViewQuiz => LocalizationStrings.Get("ViewQuiz", _currentLanguage);
        
        // Quiz Page
        public string NextQuestion => LocalizationStrings.Get("NextQuestion", _currentLanguage);
        
        // Admin Page
        public string AddNewQuiz => LocalizationStrings.Get("AddNewQuiz", _currentLanguage);
        public string Edit => LocalizationStrings.Get("Edit", _currentLanguage);
        public string Delete => LocalizationStrings.Get("Delete", _currentLanguage);
        
        // Edit Profile Page
        public string EditProfileTitle => LocalizationStrings.Get("EditProfileTitle", _currentLanguage);
        public string ChangePhoto => LocalizationStrings.Get("ChangePhoto", _currentLanguage);
        public string Username => LocalizationStrings.Get("Username", _currentLanguage);
        public string UsernamePlaceholder => LocalizationStrings.Get("UsernamePlaceholder", _currentLanguage);
        public string BioDescription => LocalizationStrings.Get("BioDescription", _currentLanguage);
        public string BioPlaceholder => LocalizationStrings.Get("BioPlaceholder", _currentLanguage);
        public string SaveChanges => LocalizationStrings.Get("SaveChanges", _currentLanguage);
        public string Cancel => LocalizationStrings.Get("Cancel", _currentLanguage);

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
