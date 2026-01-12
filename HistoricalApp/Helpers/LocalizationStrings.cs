using System.Collections.Generic;

namespace HistoricalApp.Helpers
{
    public static class LocalizationStrings
    {
        private static Dictionary<string, Dictionary<string, string>> _translations = new()
        {
            ["en"] = new Dictionary<string, string>
            {
                ["Language"] = "Language",
                ["YourProfile"] = "Your Profile",
                ["EditProfile"] = "Edit Profile",
                ["UserName"] = "User Name",
                ["Rank"] = "Rank",
                ["TotalPoints"] = "Total Points",
                ["AdminPanel"] = "Admin Panel",
                ["Logout"] = "Logout",
                ["Home"] = "Home",
                ["Profile"] = "Profile",
                ["Leaderboard"] = "Leaderboard",
            },
            ["bg"] = new Dictionary<string, string>
            {
                ["Language"] = "Език",
                ["YourProfile"] = "Вашият Профил",
                ["EditProfile"] = "Редактиране на Профила",
                ["UserName"] = "Потребителско Име",
                ["Rank"] = "Ранг",
                ["TotalPoints"] = "Общо Точки",
                ["AdminPanel"] = "Админ Панел",
                ["Logout"] = "Изход",
                ["Home"] = "Начало",
                ["Profile"] = "Профил",
                ["Leaderboard"] = "Класация",
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
