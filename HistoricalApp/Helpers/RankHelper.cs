namespace HistoricalApp.Models
{
    public static class RankCalculator
    {
        public static string GetRankFromPoints(int points)
        {
            if (points < 100)
                return "Bronze";
            if (points < 250)
                return "Silver";
            if (points < 500)
                return "Gold";
            if (points < 1000)
                return "Diamond";

            return "Historian";
        }
    }
}
