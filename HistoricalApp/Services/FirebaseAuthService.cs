using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HistoricalApp.Services
{
    public class FirebaseAuthService
    {
        private const string ApiKey = "AIzaSyCG9gAuv2a73_mcLwCCJiVP4x6nUbkbnmY";
        private readonly HttpClient _httpClient;

        public FirebaseAuthService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> RegisterUserAsync(string email, string password)
        {
            var requestUri = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={ApiKey}";
            var body = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUri, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Registration failed: {error}");
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> LoginUserAsync(string email, string password)
        {
            var requestUri = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={ApiKey}";
            var body = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUri, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Login failed: {error}");
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
