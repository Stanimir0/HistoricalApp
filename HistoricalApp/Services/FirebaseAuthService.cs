using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HistoricalApp.Services
{
    public class FirebaseAuthService
    {
        private const string ApiKey = "AIzaSyCG9gAuv2a73_mcLwCCJiVP4x6nUbkbnmY";
        private const string DatabaseUrl = "https://historical-f19c6-default-rtdb.europe-west1.firebasedatabase.app/";
        private readonly HttpClient _httpClient;

        public FirebaseAuthService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> RegisterUserAsync(string email, string password, string role = "User")
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

            var responseBody = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(responseBody).RootElement;
            var userId = json.GetProperty("localId").GetString();

           
            var roleData = new { email, role };
            var roleJson = JsonSerializer.Serialize(roleData);
            var dbUri = $"{DatabaseUrl}users/{userId}.json";
            var dbResponse = await _httpClient.PutAsync(dbUri, new StringContent(roleJson, Encoding.UTF8, "application/json"));

            if (!dbResponse.IsSuccessStatusCode)
            {
                var dbError = await dbResponse.Content.ReadAsStringAsync();
                throw new Exception($"Failed to save role in DB: {dbError}");
            }

            return responseBody;
        }

        public async Task<string> GetUserRoleAsync(string idToken)
        {
            try
            {
                
                var requestUri = $"https://identitytoolkit.googleapis.com/v1/accounts:lookup?key={ApiKey}";
                var body = new { idToken };
                var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(requestUri, content);

                if (!response.IsSuccessStatusCode)
                    return "User";

                var responseBody = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(responseBody).RootElement;
                var userId = json.GetProperty("users")[0].GetProperty("localId").GetString();

               
                var dbUri = $"{DatabaseUrl}users/{userId}/role.json?auth={idToken}";
                var dbResponse = await _httpClient.GetAsync(dbUri);

                if (!dbResponse.IsSuccessStatusCode)
                    return "User";

                var roleRaw = await dbResponse.Content.ReadAsStringAsync();

              
                var role = roleRaw.Trim('"', ' ', '\r', '\n', '\t').Replace("\\", "");

                Console.WriteLine($"[DEBUG] Role fetched from Firebase: '{role}'");

                return string.IsNullOrWhiteSpace(role) ? "User" : role;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Role Fetch Error] {ex.Message}");
                return "User";
            }
        }


        public async Task<(string Token, string Role)> LoginUserAsync(string email, string password)
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

            var responseBody = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(responseBody).RootElement;
            var userId = json.GetProperty("localId").GetString();
            var token = json.GetProperty("idToken").GetString();

           
            var dbUri = $"{DatabaseUrl}users/{userId}/role.json?auth={token}";
            var dbResponse = await _httpClient.GetAsync(dbUri);

            string role = "User"; 
            if (dbResponse.IsSuccessStatusCode)
            {
                role = await dbResponse.Content.ReadAsStringAsync();
                role = role.Trim('"');
            }

            return (token, role);
        }
    }
}
