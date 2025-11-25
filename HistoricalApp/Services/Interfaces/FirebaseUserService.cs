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
            _client = new FirebaseClient("YOUR_FIREBASE_DB_URL_HERE");
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
    }
}
