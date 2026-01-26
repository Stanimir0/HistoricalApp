using System.Collections.Generic;

namespace HistoricalApp.Helpers
{
    public static class LocalizationStrings
    {
        private static Dictionary<string, Dictionary<string, string>> _translations = new()
        {
            ["en"] = new Dictionary<string, string>
            {
                // Common
                ["Language"] = "Language",
                ["Back"] = "← Back",
                
                // Profile Page
                ["YourProfile"] = "Your Profile",
                ["EditProfile"] = "Edit Profile",
                ["UserName"] = "User Name",
                ["Rank"] = "Rank",
                ["TotalPoints"] = "Total Points",
                ["AdminPanel"] = "Admin Panel",
                ["Logout"] = "Logout",
                
                // Bottom Navigation
                ["Home"] = "Home",
                ["Profile"] = "Profile",
                ["Leaderboard"] = "Leaderboard",
                ["Ranking"] = "Ranking",
                
                // Home Page
                ["WelcomeBack"] = "Welcome back",
                ["Explorer"] = "Explorer",
                ["Battles"] = "Battles",
                ["Events"] = "Events",
                ["People"] = "People",
                ["Top10"] = "Top 10",
                
                // Leaderboard Page
                ["Points"] = "PTS",
                
                // Login Page
                ["HistoricalApp"] = "Historical App",
                ["WelcomeBackTitle"] = "Welcome Back",
                ["Email"] = "Email",
                ["EmailPlaceholder"] = "Enter your email",
                ["Password"] = "Password",
                ["PasswordPlaceholder"] = "Enter your password",
                ["Login"] = "LOGIN",
                ["DontHaveAccount"] = "Don't have an account?",
                ["CreateAccount"] = "Create Account",
                
                // Register Page
                ["JoinUs"] = "Join Us",
                ["CreateYourAccount"] = "Create your account",
                ["ConfirmPassword"] = "Confirm Password",
                ["ConfirmPasswordPlaceholder"] = "Confirm your password",
                ["ChoosePasswordPlaceholder"] = "Choose a password",
                ["Register"] = "REGISTER",
                ["BackToLogin"] = "Back to Login",
                
                // Quiz Selection Page
                ["ViewQuiz"] = "View Quiz",
                
                // Quiz Page
                ["NextQuestion"] = "Next Question",
                
                // Admin Page
                ["AddNewQuiz"] = "Add New Quiz",
                ["Edit"] = "Edit",
                ["Delete"] = "Delete",
                
                // Edit Profile Page
                ["EditProfileTitle"] = "Edit Profile",
                ["ChangePhoto"] = "Change Photo",
                ["Username"] = "Username",
                ["UsernamePlaceholder"] = "Enter username",
                ["BioDescription"] = "Bio / Description",
                ["BioPlaceholder"] = "Tell us about yourself...",
                ["SaveChanges"] = "Save Changes",
                ["Cancel"] = "Cancel",
            },
            ["bg"] = new Dictionary<string, string>
            {
                // Common
                ["Language"] = "Език",
                ["Back"] = "← Назад",
                
                // Profile Page
                ["YourProfile"] = "Вашият Профил",
                ["EditProfile"] = "Редактиране на Профила",
                ["UserName"] = "Потребителско Име",
                ["Rank"] = "Ранг",
                ["TotalPoints"] = "Общо Точки",
                ["AdminPanel"] = "Админ Панел",
                ["Logout"] = "Изход",
                
                // Bottom Navigation
                ["Home"] = "Начало",
                ["Profile"] = "Профил",
                ["Leaderboard"] = "Класация",
                ["Ranking"] = "Класация",
                
                // Home Page
                ["WelcomeBack"] = "Добре дошли отново",
                ["Explorer"] = "Изследовател",
                ["Battles"] = "Битки",
                ["Events"] = "Събития",
                ["People"] = "Личности",
                ["Top10"] = "Топ 10",
                
                // Leaderboard Page
                ["Points"] = "ТЧК",
                
                // Login Page
                ["HistoricalApp"] = "Исторично",
                ["WelcomeBackTitle"] = "Добре дошли отново",
                ["Email"] = "Имейл",
                ["EmailPlaceholder"] = "Въведете вашия имейл",
                ["Password"] = "Парола",
                ["PasswordPlaceholder"] = "Въведете вашата парола",
                ["Login"] = "ВХОД",
                ["DontHaveAccount"] = "Нямате акаунт?",
                ["CreateAccount"] = "Създайте акаунт",
                
                // Register Page
                ["JoinUs"] = "Присъединете се",
                ["CreateYourAccount"] = "Създайте вашия акаунт",
                ["ConfirmPassword"] = "Потвърдете парола",
                ["ConfirmPasswordPlaceholder"] = "Потвърдете вашата парола",
                ["ChoosePasswordPlaceholder"] = "Изберете парола",
                ["Register"] = "РЕГИСТРАЦИЯ",
                ["BackToLogin"] = "Обратно към вход",
                
                // Quiz Selection Page
                ["ViewQuiz"] = "Виж теста",
                
                // Quiz Page
                ["NextQuestion"] = "Следващ въпрос",
                
                // Admin Page
                ["AddNewQuiz"] = "Добави нов тест",
                ["Edit"] = "Редактирай",
                ["Delete"] = "Изтрий",
                
                // Edit Profile Page
                ["EditProfileTitle"] = "Редактиране на профил",
                ["ChangePhoto"] = "Смяна на снимка",
                ["Username"] = "Потребителско име",
                ["UsernamePlaceholder"] = "Въведете потребителско име",
                ["BioDescription"] = "Биография / Описание",
                ["BioPlaceholder"] = "Разкажете за себе си...",
                ["SaveChanges"] = "Запази промените",
                ["Cancel"] = "Отказ",
            }
        };

        public static string Get(string key, string languageCode = "en")
        {
            if (_translations.ContainsKey(languageCode) && 
                _translations[languageCode].ContainsKey(key))
            {
                return _translations[languageCode][key];
            }
            return key; // Fallback to key if translation not found
        }
    }
}
