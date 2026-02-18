using HistoricalApp.Models;

namespace HistoricalApp.Services
{
    public static class DailyMissionService
    {
        private static readonly Random _random = new();

        // 5 hardcoded daily missions
        private static readonly List<DailyMission> MissionPool = new()
        {
            new DailyMission { Id = "complete_1_quiz", Title = "Quiz Starter", Description = "Complete 1 quiz", CoinReward = 25, IconEmoji = "📝" },
            new DailyMission { Id = "complete_3_quizzes", Title = "Quiz Master", Description = "Complete 3 quizzes today", CoinReward = 75, IconEmoji = "🏆" },
            new DailyMission { Id = "perfect_score", Title = "Perfectionist", Description = "Get 100% in any quiz", CoinReward = 100, IconEmoji = "🌟" },
            new DailyMission { Id = "complete_timed", Title = "Speed Demon", Description = "Complete a timed quiz", CoinReward = 50, IconEmoji = "⏱️" },
            new DailyMission { Id = "play_battles", Title = "Battle Ready", Description = "Play a Battles category quiz", CoinReward = 30, IconEmoji = "⚔️" },
        };

        /// <summary>
        /// Gets today's single daily mission. Picks a new random one if the day changed.
        /// </summary>
        public static List<DailyMission> GetDailyMissions(User user)
        {
            var today = DateTime.UtcNow.Date;

            // Reset if new day
            if (user.LastDailyReset.Date != today)
            {
                ResetDailyMissions(user);
            }

            var missions = new List<DailyMission>();
            var m = MissionPool.FirstOrDefault(m => m.Id == user.DailyMission1Id);
            if (m != null)
            {
                var copy = CloneMission(m);
                copy.IsCompleted = user.DailyMission1Done;
                missions.Add(copy);
            }

            return missions;
        }

        /// <summary>
        /// Picks 1 random mission from the pool of 5.
        /// </summary>
        private static void ResetDailyMissions(User user)
        {
            var selected = MissionPool[_random.Next(MissionPool.Count)];

            user.DailyMission1Id = selected.Id;
            user.DailyMission1Done = false;
            user.DailyMission2Id = string.Empty;
            user.DailyMission2Done = false;
            user.DailyMission3Id = string.Empty;
            user.DailyMission3Done = false;

            // Reset daily tracking
            user.QuizzesCompletedToday = 0;
            user.HighestScoreToday = 0;
            user.CompletedTimedQuizToday = false;
            user.LastPlayedCategory = string.Empty;
            user.LastDailyReset = DateTime.UtcNow;
        }

        /// <summary>
        /// Checks mission progress after a quiz. Returns coins awarded.
        /// </summary>
        public static int CheckMissionProgress(User user, Quiz completedQuiz, int score, int totalQuestions)
        {
            int totalCoins = 0;
            bool isPerfect = totalQuestions > 0 && score >= completedQuiz.Points * totalQuestions;

            // Update tracking
            user.QuizzesCompletedToday++;
            if (score > user.HighestScoreToday) user.HighestScoreToday = score;
            if (completedQuiz.IsTimeBased) user.CompletedTimedQuizToday = true;
            user.LastPlayedCategory = completedQuiz.Category;

            // Check the single daily mission
            if (!user.DailyMission1Done)
            {
                var mission = MissionPool.FirstOrDefault(m => m.Id == user.DailyMission1Id);
                if (mission != null)
                {
                    bool completed = user.DailyMission1Id switch
                    {
                        "complete_1_quiz" => user.QuizzesCompletedToday >= 1,
                        "complete_3_quizzes" => user.QuizzesCompletedToday >= 3,
                        "perfect_score" => isPerfect,
                        "complete_timed" => user.CompletedTimedQuizToday,
                        "play_battles" => completedQuiz.Category.Equals("Battles", StringComparison.OrdinalIgnoreCase),
                        _ => false
                    };

                    if (completed)
                    {
                        user.DailyMission1Done = true;
                        totalCoins = mission.CoinReward;
                    }
                }
            }

            if (totalCoins > 0)
                user.Currency += totalCoins;

            return totalCoins;
        }

        private static DailyMission CloneMission(DailyMission source)
        {
            return new DailyMission
            {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
                CoinReward = source.CoinReward,
                IconEmoji = source.IconEmoji,
                IsCompleted = source.IsCompleted
            };
        }
    }
}
