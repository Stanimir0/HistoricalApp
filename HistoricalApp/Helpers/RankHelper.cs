namespace HistoricalApp.Models
{
    public static class RankCalculator
    {
        public static string GetRankFromPoints(int points)
        {
            return points switch
            {
                < 100 => "Bronze",
                < 250 => "Silver",
                < 500 => "Gold",
                < 1000 => "Diamond",
                _ => "Historian"
            };
        }
    }
}
