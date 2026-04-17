namespace HistoricalApp.Models
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public string ProfileImage { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int TotalPoints { get; set; } = 0;
        public int HighestScore { get; set; } = 0;

        // Permanent XP for leveling — never resets (unlike TotalPoints used for leaderboards)
        public int TotalXP { get; set; } = 0;

        // Currency system for shop
        public int Currency { get; set; } = 0;

        // Shop customization
        public List<string> PurchasedItems { get; set; } = new List<string>();
        public string EquippedBadge { get; set; } = string.Empty;
        public string EquippedBorder { get; set; } = string.Empty;
        public string EquippedTheme { get; set; } = string.Empty;

        // Time-based leaderboard tracking
        public int DailyPoints { get; set; } = 0;
        public int WeeklyPoints { get; set; } = 0;
        public int MonthlyPoints { get; set; } = 0;
        public DateTime LastPointsReset { get; set; } = DateTime.UtcNow;

        public string Rank { get; private set; } = "Bronze";

        // === NEW: Leveling System ===
        public int Level { get; set; } = 1;

        // === NEW: Streaks ===
        public int Streak { get; set; } = 0;
        public DateTime LastActivityDate { get; set; } = DateTime.MinValue;

        // === NEW: Daily Missions ===
        public int QuizzesCompletedToday { get; set; } = 0;
        public int HighestScoreToday { get; set; } = 0;
        public bool CompletedTimedQuizToday { get; set; } = false;
        public string LastPlayedCategory { get; set; } = string.Empty;
        public DateTime LastDailyReset { get; set; } = DateTime.MinValue;
        public string DailyMission1Id { get; set; } = string.Empty;
        public string DailyMission2Id { get; set; } = string.Empty;
        public string DailyMission3Id { get; set; } = string.Empty;
        public string DailyMission4Id { get; set; } = string.Empty;
        public string DailyMission5Id { get; set; } = string.Empty;
        public bool DailyMission1Done { get; set; } = false;
        public bool DailyMission2Done { get; set; } = false;
        public bool DailyMission3Done { get; set; } = false;
        public bool DailyMission4Done { get; set; } = false;
        public bool DailyMission5Done { get; set; } = false;

        // === NEW: Secret Badges ===
        public List<string> SecretBadges { get; set; } = new List<string>();
        public string EquippedSecretBadge { get; set; } = string.Empty;

        // === NEW: Hint Inventory ===
        public int HintFiftyFifty { get; set; } = 0;
        public int HintDoublePoints { get; set; } = 0;

        public void RecalculateRank()
        {
            Rank = RankCalculator.GetRankFromPoints(TotalPoints);
        }
    }
}
