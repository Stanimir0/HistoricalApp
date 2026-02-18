namespace HistoricalApp.Models
{
    public class DailyMission
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CoinReward { get; set; } = 0;
        public bool IsCompleted { get; set; } = false;
        public string IconEmoji { get; set; } = "📋";
    }
}
