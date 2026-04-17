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
        public string Loading => LocalizationStrings.Get("Loading", _currentLanguage);
        public string Error => LocalizationStrings.Get("Error", _currentLanguage);
        public string Success => LocalizationStrings.Get("Success", _currentLanguage);
        public string OK => LocalizationStrings.Get("OK", _currentLanguage);
        public string Yes => LocalizationStrings.Get("Yes", _currentLanguage);
        public string No => LocalizationStrings.Get("No", _currentLanguage);
        public string Confirm => LocalizationStrings.Get("Confirm", _currentLanguage);
        
        // Profile Page
        public string YourProfile => LocalizationStrings.Get("YourProfile", _currentLanguage);
        public string EditProfile => LocalizationStrings.Get("EditProfile", _currentLanguage);
        public string UserName => LocalizationStrings.Get("UserName", _currentLanguage);
        public string Rank => LocalizationStrings.Get("Rank", _currentLanguage);
        public string TotalPoints => LocalizationStrings.Get("TotalPoints", _currentLanguage);
        public string HighestScore => LocalizationStrings.Get("HighestScore", _currentLanguage);
        public string AdminPanel => LocalizationStrings.Get("AdminPanel", _currentLanguage);
        public string Logout => LocalizationStrings.Get("Logout", _currentLanguage);
        public string Coins => LocalizationStrings.Get("Coins", _currentLanguage);
        public string SecretBadgesLabel => LocalizationStrings.Get("SecretBadges", _currentLanguage);
        public string GiftCoins => LocalizationStrings.Get("GiftCoins", _currentLanguage);
        public string LanguageLabel => LocalizationStrings.Get("LanguageLabel", _currentLanguage);
        public string NoStreakYet => LocalizationStrings.Get("NoStreakYet", _currentLanguage);
        public string DayStreak => LocalizationStrings.Get("DayStreak", _currentLanguage);
        public string Level => LocalizationStrings.Get("Level", _currentLanguage);
        
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
        public string DailyMissions => LocalizationStrings.Get("DailyMissions", _currentLanguage);
        public string PlayQuiz => LocalizationStrings.Get("PlayQuiz", _currentLanguage);
        
        // Leaderboard Page
        public string Points => LocalizationStrings.Get("Points", _currentLanguage);
        public string Daily => LocalizationStrings.Get("Daily", _currentLanguage);
        public string Weekly => LocalizationStrings.Get("Weekly", _currentLanguage);
        public string Monthly => LocalizationStrings.Get("Monthly", _currentLanguage);
        public string Reward => LocalizationStrings.Get("Reward", _currentLanguage);
        public string YourRank => LocalizationStrings.Get("YourRank", _currentLanguage);
        
        // Shop Page
        public string Currency => LocalizationStrings.Get("Currency", _currentLanguage);
        public string Purchase => LocalizationStrings.Get("Purchase", _currentLanguage);
        public string Purchased => LocalizationStrings.Get("Purchased", _currentLanguage);
        public string InsufficientFunds => LocalizationStrings.Get("InsufficientFunds", _currentLanguage);
        public string Owned => LocalizationStrings.Get("Owned", _currentLanguage);
        public string AlreadyOwned => LocalizationStrings.Get("AlreadyOwned", _currentLanguage);
        public string AlreadyOwnedMsg => LocalizationStrings.Get("AlreadyOwnedMsg", _currentLanguage);
        public string PurchaseFailed => LocalizationStrings.Get("PurchaseFailed", _currentLanguage);
        public string PurchaseError => LocalizationStrings.Get("PurchaseError", _currentLanguage);
        public string MustBeLoggedIn => LocalizationStrings.Get("MustBeLoggedIn", _currentLanguage);
        
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
        public string DoublePointsActive => LocalizationStrings.Get("DoublePointsActive", _currentLanguage);
        public string FiftyFifty => LocalizationStrings.Get("FiftyFifty", _currentLanguage);
        public string DoublePointsLabel => LocalizationStrings.Get("DoublePoints", _currentLanguage);
        
        // Quiz Result Page
        public string QuizCompleted => LocalizationStrings.Get("QuizCompleted", _currentLanguage);
        public string LevelUp => LocalizationStrings.Get("LevelUp", _currentLanguage);
        public string SecretBadgeUnlocked => LocalizationStrings.Get("SecretBadgeUnlocked", _currentLanguage);
        public string DoublePointsWereActive => LocalizationStrings.Get("DoublePointsWereActive", _currentLanguage);
        public string BackToHome => LocalizationStrings.Get("BackToHome", _currentLanguage);
        public string PlayAnotherQuiz => LocalizationStrings.Get("PlayAnotherQuiz", _currentLanguage);
        
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
        public string SelectBadge => LocalizationStrings.Get("SelectBadge", _currentLanguage);
        public string SelectBorder => LocalizationStrings.Get("SelectBorder", _currentLanguage);
        public string EquipSecretBadge => LocalizationStrings.Get("EquipSecretBadge", _currentLanguage);
        public string SelectTheme => LocalizationStrings.Get("SelectTheme", _currentLanguage);
        public string RemoveBadge => LocalizationStrings.Get("RemoveBadge", _currentLanguage);
        public string RemoveBorder => LocalizationStrings.Get("RemoveBorder", _currentLanguage);
        public string RemoveSecretBadge => LocalizationStrings.Get("RemoveSecretBadge", _currentLanguage);
        public string ResetTheme => LocalizationStrings.Get("ResetTheme", _currentLanguage);
        public string NoBadgesPurchased => LocalizationStrings.Get("NoBadgesPurchased", _currentLanguage);
        public string NoBordersPurchased => LocalizationStrings.Get("NoBordersPurchased", _currentLanguage);
        public string NoThemesPurchased => LocalizationStrings.Get("NoThemesPurchased", _currentLanguage);
        
        // Category Selection Page
        public string ChooseCategory => LocalizationStrings.Get("ChooseCategory", _currentLanguage);
        public string Characters => LocalizationStrings.Get("Characters", _currentLanguage);
        
        // Quiz Info Page
        public string StartQuiz => LocalizationStrings.Get("StartQuiz", _currentLanguage);
        
        // Gift Coins
        public string GiftCoinsTitle => LocalizationStrings.Get("GiftCoinsTitle", _currentLanguage);
        public string EnterFriendUsername => LocalizationStrings.Get("EnterFriendUsername", _currentLanguage);
        public string Send => LocalizationStrings.Get("Send", _currentLanguage);
        public string InvalidAmount => LocalizationStrings.Get("InvalidAmount", _currentLanguage);
        public string NotEnoughCoins => LocalizationStrings.Get("NotEnoughCoins", _currentLanguage);
        public string CantGiftSelf => LocalizationStrings.Get("CantGiftSelf", _currentLanguage);
        public string ConfirmGift => LocalizationStrings.Get("ConfirmGift", _currentLanguage);
        public string WaitForProfile => LocalizationStrings.Get("WaitForProfile", _currentLanguage);
        
        // Language
        public string LanguageChanged => LocalizationStrings.Get("LanguageChanged", _currentLanguage);
        public string UIUpdated => LocalizationStrings.Get("UIUpdated", _currentLanguage);

        /// <summary>
        /// Gets a formatted translation string with parameters (for strings with placeholders like {0}, {1}).
        /// </summary>
        public string GetFormatted(string key, params object[] args)
        {
            var template = LocalizationStrings.Get(key, _currentLanguage);
            return string.Format(template, args);
        }

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
