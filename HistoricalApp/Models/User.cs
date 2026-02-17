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

        // Currency system for shop
        public int Currency { get; set; } = 0;

        // Shop customization
        public List<string> PurchasedItems { get; set; } = new List<string>();
        public string EquippedBadge { get; set; } = string.Empty;
        public string EquippedBorder { get; set; } = string.Empty;

        // Time-based leaderboard tracking
        public int DailyPoints { get; set; } = 0;
        public int WeeklyPoints { get; set; } = 0;
        public int MonthlyPoints { get; set; } = 0;
        public DateTime LastPointsReset { get; set; } = DateTime.UtcNow;

        public string Rank { get; private set; } = "Bronze";

        public void RecalculateRank()
        {
            Rank = RankCalculator.GetRankFromPoints(TotalPoints);
        }
    }
}
