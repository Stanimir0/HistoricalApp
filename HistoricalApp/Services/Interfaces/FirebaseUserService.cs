using Firebase.Database;
using Firebase.Database.Query;
using HistoricalApp.Models;

namespace HistoricalApp.Services
{
    public class FirebaseUserService
    {
        private readonly FirebaseClient _client;

        public FirebaseUserService()
        {
            _client = new FirebaseClient("https://historical-f19c6-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task<User?> GetUserByIdAsync(string userId)
        {
            var user = await _client
                .Child("users")
                .Child(userId)
                .OnceSingleAsync<User>();

            return user;
        }

        public async Task UpdateUserAsync(string userId, User updatedUser)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
            }

            await _client
                .Child("users")
                .Child(userId)
                .PutAsync(updatedUser);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _client
                .Child("users")
                .OnceAsync<User>();

            return users.Select(u => u.Object).ToList();
        }

        // Currency management
        public async Task<bool> UpdateUserCurrencyAsync(string userId, int amount)
        {
            try
            {
                var user = await GetUserByIdAsync(userId);
                if (user == null) return false;

                user.Currency += amount;
                if (user.Currency < 0) user.Currency = 0; // Prevent negative currency

                await UpdateUserAsync(userId, user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Shop purchase
        public async Task<bool> PurchaseItemAsync(string userId, ShopItem item)
        {
            try
            {
                var user = await GetUserByIdAsync(userId);
                if (user == null || user.Currency < item.Price) return false;

                user.Currency -= item.Price;
                await UpdateUserAsync(userId, user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Get leaderboard by period
        public async Task<List<User>> GetLeaderboardByPeriodAsync(string period)
        {
            var allUsers = await GetAllUsersAsync();
            if (allUsers == null || allUsers.Count == 0) return new List<User>();

            return period.ToLower() switch
            {
                "daily" => allUsers.OrderByDescending(u => u.DailyPoints).ToList(),
                "weekly" => allUsers.OrderByDescending(u => u.WeeklyPoints).ToList(),
                "monthly" => allUsers.OrderByDescending(u => u.MonthlyPoints).ToList(),
                _ => allUsers.OrderByDescending(u => u.TotalPoints).ToList()
            };
        }

        // Reset periodic points (call this on period transitions)
        public async Task ResetPeriodicPointsAsync(string resetType)
        {
            var allUsers = await GetAllUsersAsync();
            if (allUsers == null) return;

            foreach (var user in allUsers)
            {
                switch (resetType.ToLower())
                {
                    case "daily":
                        user.DailyPoints = 0;
                        break;
                    case "weekly":
                        user.WeeklyPoints = 0;
                        break;
                    case "monthly":
                        user.MonthlyPoints = 0;
                        break;
                }
                user.LastPointsReset = DateTime.UtcNow;
                await UpdateUserAsync(user.Id, user);
            }
        }

        // Award points (updates all point types)
        public async Task AwardPointsAsync(string userId, int points)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null) return;

            user.TotalPoints += points;
            user.DailyPoints += points;
            user.WeeklyPoints += points;
            user.MonthlyPoints += points;

            await UpdateUserAsync(userId, user);
        }
    }
}
