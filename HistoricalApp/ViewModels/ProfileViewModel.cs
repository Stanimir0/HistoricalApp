using Firebase.Database;
using Firebase.Database.Query;
using HistoricalApp.Models;
using Microsoft.Maui.Storage;
using System.Threading.Tasks;

namespace HistoricalApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly FirebaseClient _client;

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        public Command LoadUserCommand { get; }

        public ProfileViewModel()
        {
            _client = new FirebaseClient("https://historical-f19c6-default-rtdb.europe-west1.firebasedatabase.app/");
            LoadUserCommand = new Command(async () => await LoadUserAsync());
        }

        public async Task LoadUserAsync()
        {
            var userId = Preferences.Get("UserId", string.Empty);
            if (string.IsNullOrEmpty(userId))
                return;

            try
            {
                var user = await _client.Child("users")
                                        .Child(userId)
                                        .OnceSingleAsync<User>();

                if (user != null)
                {
                    user.RecalculateRank();
                    CurrentUser = user;
                }
            }
            catch
            {
                // ignore for now
            }
        }
    }
}
