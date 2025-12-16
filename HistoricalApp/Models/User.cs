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

        public string Rank { get; private set; } = "Bronze";

        public void RecalculateRank()
        {
            Rank = RankCalculator.GetRankFromPoints(TotalPoints);
        }
    }
}
