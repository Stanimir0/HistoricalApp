using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Firebase.Database;
using Firebase.Database.Query;
using HistoricalApp.Models;

namespace HistoricalApp.Services
{
    public class FirebaseAuthService
    {
        private const string ApiKey = "AIzaSyCG9gAuv2a73_mcLwCCJiVP4x6nUbkbnmY";
        private readonly HttpClient _httpClient;
        private readonly FirebaseClient _firebaseClient;

        public FirebaseAuthService()
        {
            _httpClient = new HttpClient();
            _firebaseClient = new FirebaseClient("https://historical-f19c6-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        // ✅ REGISTER
        public async Task<string> RegisterUserAsync(string email, string password)
        {
            var uri = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={ApiKey}";
            var body = new { email, password, returnSecureToken = true };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Registration failed: {result}");

            var json = JsonDocument.Parse(result);
            var userId = json.RootElement.GetProperty("localId").GetString();

            Preferences.Set("UserId", userId);
            Preferences.Set("UserEmail", email);

            await _firebaseClient.Child("users").Child(userId).PutAsync(new User
            {
                Id = userId!,
                Email = email,
                Role = "User",
                TotalPoints = 0,
                TotalXP = 0
            });

            return userId!;
        }

        // ✅ LOGIN
        public async Task<string> LoginUserAsync(string email, string password)
        {
            var uri = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={ApiKey}";
            var body = new { email, password, returnSecureToken = true };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Login failed: {result}");

            var json = JsonDocument.Parse(result);
            var userId = json.RootElement.GetProperty("localId").GetString();

            Preferences.Set("UserId", userId);
            Preferences.Set("UserEmail", email);

            var existing = await _firebaseClient.Child("users").Child(userId).OnceSingleAsync<User>();
            if (existing == null)
            {
                await _firebaseClient.Child("users").Child(userId).PutAsync(new User
                {
                    Id = userId!,
                    Email = email,
                    Role = "User",
                    TotalPoints = 0,
                    TotalXP = 0
                });
            }

            return userId!;
        }

        // ✅ ROLE CHECK
        public async Task<string> GetUserRoleAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return "User";

            try
            {
                var user = await _firebaseClient.Child("users").Child(userId).OnceSingleAsync<User>();
                return user?.Role ?? "User";
            }
            catch
            {
                return "User";
            }
        }
    }
}
