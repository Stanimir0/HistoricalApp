using HistoricalApp.Models;

namespace HistoricalApp.Services
{
    public static class SecretBadgeService
    {
        private static readonly Random _random = new();

        public class SecretBadge
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Emoji { get; set; } = string.Empty;
            public double DropChance { get; set; } // 0.0 to 1.0 (e.g., 0.01 = 1%)
        }

        private static readonly List<SecretBadge> AllSecretBadges = new()
        {
            new SecretBadge { Id = "secret_ancient_relic", Name = "Ancient Relic", Emoji = "🏺", DropChance = 0.005 },    // 0.5% (1 in 200)
            new SecretBadge { Id = "secret_phoenix_feather", Name = "Phoenix Feather", Emoji = "🔮", DropChance = 0.0025 }, // 0.25% (1 in 400)
            new SecretBadge { Id = "secret_time_traveler", Name = "Time Traveler", Emoji = "⌛", DropChance = 0.001 },     // 0.1% (1 in 1000)
            new SecretBadge { Id = "secret_historians_eye", Name = "Historian's Eye", Emoji = "👁️", DropChance = 0.0001 }   // 0.01% (1 in 10,000)
        };

        /// <summary>
        /// Rolls for a secret badge. Returns the badge if won, null otherwise.
        /// Does not award duplicates.
        /// </summary>
        public static SecretBadge? TryAwardSecretBadge(User user)
        {
            if (user.SecretBadges == null)
                user.SecretBadges = new List<string>();

            foreach (var badge in AllSecretBadges)
            {
                // Skip if already owned
                if (user.SecretBadges.Contains(badge.Id))
                    continue;

                double roll = _random.NextDouble();
                if (roll < badge.DropChance)
                {
                    user.SecretBadges.Add(badge.Id);
                    return badge;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets badge info by ID.
        /// </summary>
        public static SecretBadge? GetBadgeById(string id)
        {
            return AllSecretBadges.FirstOrDefault(b => b.Id == id);
        }

        /// <summary>
        /// Gets all secret badges (for display purposes).
        /// </summary>
        public static List<SecretBadge> GetAllBadges()
        {
            return AllSecretBadges;
        }
    }
}
