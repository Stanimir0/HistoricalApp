using HistoricalApp.Models;

namespace HistoricalApp.Services
{
    public static class StreakService
    {
        // Streak milestones and their coin bonuses
        private static readonly Dictionary<int, int> StreakBonuses = new()
        {
            { 3, 25 },
            { 7, 75 },
            { 14, 150 },
            { 30, 500 }
        };

        /// <summary>
        /// Updates the user's streak based on their last activity date.
        /// Returns bonus coins if a milestone was hit (0 otherwise).
        /// </summary>
        public static (int newStreak, int bonusCoins) UpdateStreak(User user)
        {
            var today = DateTime.UtcNow.Date;
            var lastActivity = user.LastActivityDate.Date;

            if (lastActivity == today)
            {
                // Already played today, no streak change
                return (user.Streak, 0);
            }

            if (lastActivity == today.AddDays(-1))
            {
                // Consecutive day — increment streak
                user.Streak++;
            }
            else
            {
                // Streak broken — reset to 1 (today counts)
                user.Streak = 1;
            }

            user.LastActivityDate = DateTime.UtcNow;

            // Check if we hit a milestone
            int bonusCoins = 0;
            if (StreakBonuses.TryGetValue(user.Streak, out int bonus))
            {
                bonusCoins = bonus;
                user.Currency += bonusCoins;
            }

            return (user.Streak, bonusCoins);
        }

        /// <summary>
        /// Returns the next streak milestone and how many days until it.
        /// </summary>
        public static (int nextMilestone, int daysUntil) GetNextMilestone(int currentStreak)
        {
            foreach (var kvp in StreakBonuses.OrderBy(k => k.Key))
            {
                if (currentStreak < kvp.Key)
                {
                    return (kvp.Key, kvp.Key - currentStreak);
                }
            }
            return (0, 0); // All milestones reached
        }

        /// <summary>
        /// Gets the emoji for the current streak level.
        /// </summary>
        public static string GetStreakEmoji(int streak)
        {
            if (streak >= 30) return "🌟";
            if (streak >= 14) return "💎";
            if (streak >= 7) return "🔥";
            if (streak >= 3) return "⚡";
            return "🔥";
        }
    }
}
