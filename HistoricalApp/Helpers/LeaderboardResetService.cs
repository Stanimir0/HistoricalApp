using HistoricalApp.Services;

namespace HistoricalApp.Helpers
{
    public class LeaderboardResetService
    {
        private readonly FirebaseUserService _userService;
        private static DateTime _lastResetCheck = DateTime.MinValue;

        public LeaderboardResetService()
        {
            _userService = new FirebaseUserService();
        }

        /// <summary>
        /// Check if any period needs to be reset and perform the reset
        /// This should be called when app starts or when loading leaderboard
        /// </summary>
        public async Task CheckAndResetPeriodsAsync()
        {
            // Avoid checking too frequently (once per app session is enough)
            if ((DateTime.UtcNow - _lastResetCheck).TotalMinutes < 5)
                return;

            _lastResetCheck = DateTime.UtcNow;

            try
            {
                var allUsers = await _userService.GetAllUsersAsync();
                if (allUsers == null || allUsers.Count == 0)
                    return;

                // Get the oldest LastPointsReset to determine what needs resetting
                var oldestReset = allUsers.Min(u => u.LastPointsReset);
                var now = DateTime.UtcNow;

                bool needsDailyReset = (now - oldestReset).TotalDays >= 1;
                bool needsWeeklyReset = (now - oldestReset).TotalDays >= 7;
                bool needsMonthlyReset = now.Month != oldestReset.Month || now.Year != oldestReset.Year;

                // Reset in order: daily, weekly, monthly
                if (needsDailyReset && ShouldResetDaily(oldestReset, now))
                {
                    await _userService.ResetPeriodicPointsAsync("daily");
                    System.Diagnostics.Debug.WriteLine("Daily points reset completed");
                }

                if (needsWeeklyReset && ShouldResetWeekly(oldestReset, now))
                {
                    await _userService.ResetPeriodicPointsAsync("weekly");
                    System.Diagnostics.Debug.WriteLine("Weekly points reset completed");
                }

                if (needsMonthlyReset)
                {
                    await _userService.ResetPeriodicPointsAsync("monthly");
                    System.Diagnostics.Debug.WriteLine("Monthly points reset completed");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking/resetting periods: {ex.Message}");
            }
        }

        /// <summary>
        /// Check if daily reset should happen (midnight UTC passed)
        /// </summary>
        private bool ShouldResetDaily(DateTime lastReset, DateTime now)
        {
            return now.Date > lastReset.Date;
        }

        /// <summary>
        /// Check if weekly reset should happen (Monday 00:00 UTC passed)
        /// </summary>
        private bool ShouldResetWeekly(DateTime lastReset, DateTime now)
        {
            // Find the most recent Monday
            var lastMonday = now.Date;
            while (lastMonday.DayOfWeek != DayOfWeek.Monday)
                lastMonday = lastMonday.AddDays(-1);

            var lastResetMonday = lastReset.Date;
            while (lastResetMonday.DayOfWeek != DayOfWeek.Monday)
                lastResetMonday = lastResetMonday.AddDays(-1);

            return lastMonday > lastResetMonday;
        }
    }
}
