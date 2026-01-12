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
    }
}
