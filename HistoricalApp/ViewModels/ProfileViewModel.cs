using Firebase.Database;
using Firebase.Database.Query;
using HistoricalApp.Models;
using Microsoft.Maui.Storage;
using System.Threading.Tasks;

namespace HistoricalApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly FirebaseClient _firebaseClient;
        private const string DbUrl = "https://historical-f19c6-default-rtdb.europe-west1.firebasedatabase.app/";

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        public ProfileViewModel()
        {
            _firebaseClient = new FirebaseClient(DbUrl);
            _ = LoadUserAsync();
        }

        private async Task LoadUserAsync()
        {
            var userId = Preferences.Get("UserId", string.Empty);
            if (string.IsNullOrEmpty(userId))
                return;

            var user = await _firebaseClient.Child("users").Child(userId).OnceSingleAsync<User>();
            CurrentUser = user ?? new User { Email = "Unknown", TotalPoints = 0 };
        }
    }
}
