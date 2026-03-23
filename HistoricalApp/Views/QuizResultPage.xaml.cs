using HistoricalApp.Helpers;

namespace HistoricalApp.Views
{
    public partial class QuizResultPage : ContentPage
    {
        public int Score { get; }
        public int TotalQuestions { get; }
        public int CorrectAnswers { get; }
        public int LevelsGained { get; }
        public int CoinsFromLevelUp { get; }
        public int NewLevel { get; }
        public int StreakCount { get; }
        public int StreakBonusCoins { get; }
        public int MissionCoins { get; }
        public string SecretBadgeName { get; }
        public string SecretBadgeEmoji { get; }
        public bool WasDoublePoints { get; }

        // Translation helper
        public TranslationService Translations => TranslationService.Instance;

        // Computed properties for UI
        public string ScoreText => Translations.GetFormatted("YouScored", Score);
        public string PointsInfo => Translations.GetFormatted("CorrectAnswers", CorrectAnswers, TotalQuestions);
        public string AccuracyText => TotalQuestions > 0
            ? Translations.GetFormatted("Accuracy", CorrectAnswers * 100 / TotalQuestions)
            : Translations.GetFormatted("Accuracy", 0);

        public string StarsText
        {
            get
            {
                if (TotalQuestions == 0) return "⭐";
                double pct = (double)CorrectAnswers / TotalQuestions;
                if (pct >= 1.0) return "⭐⭐⭐";
                if (pct >= 0.7) return "⭐⭐";
                return "⭐";
            }
        }

        // Level up
        public bool ShowLevelUp => LevelsGained > 0;
        public string LevelUpText => LevelsGained > 1
            ? Translations.GetFormatted("LevelRange", NewLevel - LevelsGained, NewLevel)
            : Translations.GetFormatted("ReachedLevel", NewLevel);
        public string LevelUpCoins => CoinsFromLevelUp > 0 ? Translations.GetFormatted("CoinsEarned", CoinsFromLevelUp) : "";

        // Streak
        public bool ShowStreak => StreakCount > 0;
        public string StreakText => Translations.GetFormatted("DayStreakExcl", StreakCount);
        public bool ShowStreakBonus => StreakBonusCoins > 0;
        public string StreakBonusText => Translations.GetFormatted("BonusCoins", StreakBonusCoins);

        // Mission
        public bool ShowMissionCoins => MissionCoins > 0;
        public string MissionCoinsText => Translations.GetFormatted("DailyMissionReward", MissionCoins);

        // Secret Badge
        public bool ShowSecretBadge => !string.IsNullOrEmpty(SecretBadgeName);
        public string SecretBadgeDisplay => SecretBadgeEmoji ?? "";
        public string SecretBadgeNameText => SecretBadgeName ?? "";

        public QuizResultPage(
            int score,
            int totalQuestions,
            int correctAnswers = 0,
            int levelsGained = 0,
            int coinsFromLevelUp = 0,
            int newLevel = 0,
            int streakCount = 0,
            int streakBonusCoins = 0,
            int missionCoins = 0,
            string secretBadgeName = null,
            string secretBadgeEmoji = null,
            bool wasDoublePoints = false)
        {
            InitializeComponent();

            Score = score;
            TotalQuestions = totalQuestions;
            CorrectAnswers = correctAnswers;
            LevelsGained = levelsGained;
            CoinsFromLevelUp = coinsFromLevelUp;
            NewLevel = newLevel;
            StreakCount = streakCount;
            StreakBonusCoins = streakBonusCoins;
            MissionCoins = missionCoins;
            SecretBadgeName = secretBadgeName;
            SecretBadgeEmoji = secretBadgeEmoji;
            WasDoublePoints = wasDoublePoints;

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AnimationHelper.SlideUpFadeIn(RootLayout);
        }

        private async void OnBackHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

        private async void OnPlayAnotherClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//CategorySelectionPage");
        }
    }
}
