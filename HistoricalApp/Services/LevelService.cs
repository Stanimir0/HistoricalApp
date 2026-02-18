using HistoricalApp.Models;

namespace HistoricalApp.Services
{
    public static class LevelService
    {
        // XP thresholds for each level
        private static readonly int[] LevelThresholds = 
        { 
            0,       // Level 1
            1000,    // Level 2
            2500,    // Level 3
            5000,    // Level 4
            8000,    // Level 5
            12000,   // Level 6
            17000,   // Level 7
            23000,   // Level 8
            30000,   // Level 9
            40000    // Level 10
        };

        // Coin rewards for reaching each level
        private static readonly int[] LevelRewards = 
        { 
            0,      // Level 1 (starting)
            50,     // Level 2
            75,     // Level 3
            100,    // Level 4
            150,    // Level 5
            200,    // Level 6
            300,    // Level 7
            400,    // Level 8
            500,    // Level 9
            750     // Level 10
        };

        public static int MaxLevel => LevelThresholds.Length;

        public static int GetLevelFromXP(int totalPoints)
        {
            int level = 1;
            for (int i = LevelThresholds.Length - 1; i >= 0; i--)
            {
                if (totalPoints >= LevelThresholds[i])
                {
                    level = i + 1;
                    break;
                }
            }
            return level;
        }

        public static int GetXPForLevel(int level)
        {
            if (level < 1) return 0;
            if (level > LevelThresholds.Length) return LevelThresholds[^1];
            return LevelThresholds[level - 1];
        }

        public static int GetXPForNextLevel(int level)
        {
            if (level >= LevelThresholds.Length) return LevelThresholds[^1]; // Max level
            return LevelThresholds[level]; // level is 1-indexed, so [level] gives the NEXT level threshold
        }

        public static int GetCoinRewardForLevel(int level)
        {
            if (level < 1 || level > LevelRewards.Length) return 0;
            return LevelRewards[level - 1];
        }

        /// <summary>
        /// Checks if user's XP warrants a level-up and awards coins for each level gained.
        /// Returns the number of levels gained (0 if no level-up).
        /// </summary>
        public static (int levelsGained, int coinsAwarded, int newLevel) CheckAndProcessLevelUp(User user)
        {
            int calculatedLevel = GetLevelFromXP(user.TotalXP);
            int levelsGained = calculatedLevel - user.Level;

            if (levelsGained <= 0)
            {
                return (0, 0, user.Level);
            }

            int totalCoins = 0;
            for (int lvl = user.Level + 1; lvl <= calculatedLevel; lvl++)
            {
                totalCoins += GetCoinRewardForLevel(lvl);
            }

            user.Level = calculatedLevel;
            user.Currency += totalCoins;

            return (levelsGained, totalCoins, calculatedLevel);
        }

        /// <summary>
        /// Returns a progress fraction (0.0 - 1.0) towards the next level.
        /// </summary>
        public static double GetLevelProgress(int totalPoints, int currentLevel)
        {
            int currentThreshold = GetXPForLevel(currentLevel);
            int nextThreshold = GetXPForNextLevel(currentLevel);

            if (currentLevel >= MaxLevel) return 1.0;

            int range = nextThreshold - currentThreshold;
            if (range <= 0) return 1.0;

            int progress = totalPoints - currentThreshold;
            return Math.Clamp((double)progress / range, 0.0, 1.0);
        }
    }
}
