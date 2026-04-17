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
                ["Loading"] = "Loading...",
                ["Error"] = "Error",
                ["Success"] = "Success",
                ["OK"] = "OK",
                ["Yes"] = "Yes",
                ["No"] = "No",
                ["Confirm"] = "Confirm",
                
                // Profile Page
                ["YourProfile"] = "Your Profile",
                ["EditProfile"] = "Edit Profile",
                ["UserName"] = "User Name",
                ["Rank"] = "Rank",
                ["TotalPoints"] = "Total Points",
                ["HighestScore"] = "Highest Score",
                ["AdminPanel"] = "Admin Panel",
                ["Logout"] = "Logout",
                ["Coins"] = "Coins",
                ["SecretBadges"] = "✨ Secret Badges",
                ["GiftCoins"] = "🎁 Gift Coins to Friend",
                ["LanguageLabel"] = "Language / Език",
                ["NoStreakYet"] = "No streak yet",
                ["DayStreak"] = "day streak",
                ["Level"] = "Level",
                
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
                ["Shop"] = "Shop",
                ["DailyMissions"] = "Daily Missions",
                ["PlayQuiz"] = "Play Quiz",
                
                // Leaderboard Page
                ["Points"] = "PTS",
                ["Daily"] = "Daily",
                ["Weekly"] = "Weekly",
                ["Monthly"] = "Monthly",
                ["Reward"] = "Reward",
                ["YourRank"] = "Your Rank",
                
                // Shop Page
                ["Currency"] = "Currency",
                ["Purchase"] = "Purchase",
                ["Purchased"] = "Purchased",
                ["InsufficientFunds"] = "Insufficient Funds",
                ["Owned"] = "✓ Owned",
                ["AlreadyOwned"] = "Already Owned",
                ["AlreadyOwnedMsg"] = "You already own this item.",
                ["PurchaseConfirm"] = "Purchase {0} for {1} coins?",
                ["PurchaseConfirmConsumable"] = "Purchase {0} for {1} coins? (consumable)",
                ["PurchaseSuccess"] = "You purchased {0}! You can equip it in Edit Profile.",
                ["PurchaseSuccessConsumable"] = "You purchased {0}! Use it during a quiz.",
                ["PurchaseFailed"] = "Purchase failed. Please try again.",
                ["PurchaseError"] = "An error occurred during purchase.",
                ["NeedMoreCoins"] = "You need {0} more coins.",
                ["MustBeLoggedIn"] = "You must be logged in to purchase items.",
                
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
                ["DoublePointsActive"] = "✨ DOUBLE POINTS ACTIVE ✨",
                ["FiftyFifty"] = "🎯 50/50",
                ["DoublePoints"] = "✨ 2x Points",
                
                // Quiz Result Page
                ["QuizCompleted"] = "Quiz Completed!",
                ["YouScored"] = "You scored {0} points",
                ["CorrectAnswers"] = "{0}/{1} correct answers",
                ["Accuracy"] = "Accuracy: {0}%",
                ["LevelUp"] = "🎉 LEVEL UP!",
                ["ReachedLevel"] = "You reached Level {0}!",
                ["LevelRange"] = "Level {0} → Level {1}!",
                ["CoinsEarned"] = "+{0} coins earned!",
                ["DayStreakExcl"] = "{0} day streak!",
                ["BonusCoins"] = "+{0} bonus coins!",
                ["DailyMissionReward"] = "Daily Mission Reward: +{0} coins!",
                ["SecretBadgeUnlocked"] = "✨ SECRET BADGE UNLOCKED! ✨",
                ["DoublePointsWereActive"] = "✨ Double Points were active!",
                ["BackToHome"] = "Back to Home",
                ["PlayAnotherQuiz"] = "Play Another Quiz",
                
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
                ["SelectBadge"] = "Select Badge (Optional)",
                ["SelectBorder"] = "Select Profile Border (Optional)",
                ["EquipSecretBadge"] = "Equip Secret Badge (Optional)",
                ["SelectTheme"] = "Select App Theme",
                ["RemoveBadge"] = "Remove Badge",
                ["RemoveBorder"] = "Remove Border",
                ["RemoveSecretBadge"] = "Remove Secret Badge",
                ["ResetTheme"] = "Reset to Default Theme",
                ["NoBadgesPurchased"] = "No badges purchased yet. Visit the Shop!",
                ["NoBordersPurchased"] = "No borders purchased yet. Visit the Shop!",
                ["NoThemesPurchased"] = "No themes purchased yet.",
                
                // Category Selection Page
                ["ChooseCategory"] = "Choose a Category",
                ["Characters"] = "Characters",
                
                // Quiz Info Page
                ["Category"] = "Category: {0}",
                ["Difficulty"] = "Difficulty: {0}",
                ["PointsFormat"] = "Points: {0}",
                ["StartQuiz"] = "Start Quiz",
                
                // Gift Coins
                ["GiftCoinsTitle"] = "Gift Coins",
                ["EnterFriendUsername"] = "Enter the username of the friend:",
                ["Send"] = "Send",
                ["HowManyCoins"] = "How many coins to send to {0}?",
                ["InvalidAmount"] = "Please enter a valid amount.",
                ["NotEnoughCoins"] = "You don't have enough coins.",
                ["UserNotFound"] = "User '{0}' not found.",
                ["CantGiftSelf"] = "You can't gift coins to yourself.",
                ["ConfirmGift"] = "Confirm Gift",
                ["ConfirmGiftMsg"] = "Send {0} coins to {1}?",
                ["GiftSuccess"] = "You sent {0} coins to {1}!",
                
                // Profile
                ["WaitForProfile"] = "Please wait for your profile to load before editing.",
                
                // Language
                ["LanguageChanged"] = "Language Changed",
                ["UIUpdated"] = "UI has been updated!",
            },
            ["bg"] = new Dictionary<string, string>
            {
                // Common
                ["Language"] = "Език",
                ["Back"] = "← Назад",
                ["Loading"] = "Зареждане...",
                ["Error"] = "Грешка",
                ["Success"] = "Успех",
                ["OK"] = "ОК",
                ["Yes"] = "Да",
                ["No"] = "Не",
                ["Confirm"] = "Потвърди",
                
                // Profile Page
                ["YourProfile"] = "Вашият Профил",
                ["EditProfile"] = "Редактиране на Профила",
                ["UserName"] = "Потребителско Име",
                ["Rank"] = "Ранг",
                ["TotalPoints"] = "Общо Точки",
                ["HighestScore"] = "Най-висок резултат",
                ["AdminPanel"] = "Админ Панел",
                ["Logout"] = "Изход",
                ["Coins"] = "Монети",
                ["SecretBadges"] = "✨ Тайни Значки",
                ["GiftCoins"] = "🎁 Подари Монети",
                ["LanguageLabel"] = "Language / Език",
                ["NoStreakYet"] = "Все още няма серия",
                ["DayStreak"] = "дневна серия",
                ["Level"] = "Ниво",
                
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
                ["Shop"] = "Магазин",
                ["DailyMissions"] = "Дневни Мисии",
                ["PlayQuiz"] = "Играй Тест",
                
                // Leaderboard Page
                ["Points"] = "ТЧК",
                ["Daily"] = "Дневен",
                ["Weekly"] = "Седмичен",
                ["Monthly"] = "Месечен",
                ["Reward"] = "Награда",
                ["YourRank"] = "Твоето място",
                
                // Shop Page
                ["Currency"] = "Валута",
                ["Purchase"] = "Купи",
                ["Purchased"] = "Закупен",
                ["InsufficientFunds"] = "Недостатъчно средства",
                ["Owned"] = "✓ Притежаван",
                ["AlreadyOwned"] = "Вече притежавате",
                ["AlreadyOwnedMsg"] = "Вече притежавате този предмет.",
                ["PurchaseConfirm"] = "Купете {0} за {1} монети?",
                ["PurchaseConfirmConsumable"] = "Купете {0} за {1} монети? (консумативен)",
                ["PurchaseSuccess"] = "Купихте {0}! Можете да го екипирате в Редактиране на профила.",
                ["PurchaseSuccessConsumable"] = "Купихте {0}! Използвайте го по време на тест.",
                ["PurchaseFailed"] = "Покупката е неуспешна. Моля, опитайте отново.",
                ["PurchaseError"] = "Възникна грешка при покупката.",
                ["NeedMoreCoins"] = "Нуждаете се от още {0} монети.",
                ["MustBeLoggedIn"] = "Трябва да сте влезли, за да купувате предмети.",
                
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
                ["DoublePointsActive"] = "✨ ДВОЙНИ ТОЧКИ АКТИВНИ ✨",
                ["FiftyFifty"] = "🎯 50/50",
                ["DoublePoints"] = "✨ 2x Точки",
                
                // Quiz Result Page
                ["QuizCompleted"] = "Тестът е завършен!",
                ["YouScored"] = "Вие спечелихте {0} точки",
                ["CorrectAnswers"] = "{0}/{1} верни отговора",
                ["Accuracy"] = "Точност: {0}%",
                ["LevelUp"] = "🎉 НОВО НИВО!",
                ["ReachedLevel"] = "Достигнахте Ниво {0}!",
                ["LevelRange"] = "Ниво {0} → Ниво {1}!",
                ["CoinsEarned"] = "+{0} спечелени монети!",
                ["DayStreakExcl"] = "{0} дневна серия!",
                ["BonusCoins"] = "+{0} бонус монети!",
                ["DailyMissionReward"] = "Награда от Дневна Мисия: +{0} монети!",
                ["SecretBadgeUnlocked"] = "✨ ТАЙНА ЗНАЧКА ОТКЛЮЧЕНА! ✨",
                ["DoublePointsWereActive"] = "✨ Двойните точки бяха активни!",
                ["BackToHome"] = "Към Начало",
                ["PlayAnotherQuiz"] = "Играй Друг Тест",
                
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
                ["SelectBadge"] = "Избери Значка (По избор)",
                ["SelectBorder"] = "Избери Рамка (По избор)",
                ["EquipSecretBadge"] = "Екипирай Тайна Значка (По избор)",
                ["SelectTheme"] = "Избери Тема",
                ["RemoveBadge"] = "Премахни Значка",
                ["RemoveBorder"] = "Премахни Рамка",
                ["RemoveSecretBadge"] = "Премахни Тайна Значка",
                ["ResetTheme"] = "Нулиране към Тема по подразбиране",
                ["NoBadgesPurchased"] = "Все още няма закупени значки. Посетете Магазина!",
                ["NoBordersPurchased"] = "Все още няма закупени рамки. Посетете Магазина!",
                ["NoThemesPurchased"] = "Все още няма закупени теми.",
                
                // Category Selection Page
                ["ChooseCategory"] = "Избери Категория",
                ["Characters"] = "Личности",
                
                // Quiz Info Page
                ["Category"] = "Категория: {0}",
                ["Difficulty"] = "Трудност: {0}",
                ["PointsFormat"] = "Точки: {0}",
                ["StartQuiz"] = "Започни Тест",
                
                // Gift Coins
                ["GiftCoinsTitle"] = "Подари Монети",
                ["EnterFriendUsername"] = "Въведете потребителското име на приятеля:",
                ["Send"] = "Изпрати",
                ["HowManyCoins"] = "Колко монети да изпратите на {0}?",
                ["InvalidAmount"] = "Моля, въведете валидна сума.",
                ["NotEnoughCoins"] = "Нямате достатъчно монети.",
                ["UserNotFound"] = "Потребител '{0}' не е намерен.",
                ["CantGiftSelf"] = "Не можете да подарите монети на себе си.",
                ["ConfirmGift"] = "Потвърди Подарък",
                ["ConfirmGiftMsg"] = "Изпрати {0} монети на {1}?",
                ["GiftSuccess"] = "Изпратихте {0} монети на {1}!",
                
                // Profile
                ["WaitForProfile"] = "Моля, изчакайте профилът да се зареди преди редактиране.",
                
                // Language
                ["LanguageChanged"] = "Езикът е променен",
                ["UIUpdated"] = "UI е актуализиран!",
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
