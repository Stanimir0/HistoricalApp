using Firebase.Database;
using Firebase.Database.Query;
using HistoricalApp.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace HistoricalApp.ViewModels
{
    public class LeaderboardViewModel : BaseViewModel
    {
        private readonly FirebaseClient _client;
        public ObservableCollection<User> Users { get; set; } = new();

        public LeaderboardViewModel()
        {
            _client = new FirebaseClient("https://historical-f19c6-default-rtdb.europe-west1.firebasedatabase.app/");
            _ = LoadLeaderboardAsync();
        }

        private async Task LoadLeaderboardAsync()
        {
            var usersData = await _client.Child("users").OnceAsync<User>();
            var sorted = usersData.Select(x => x.Object).OrderByDescending(u => u.TotalPoints);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Users.Clear();
                foreach (var u in sorted)
                    Users.Add(u);
            });
        }
    }
}
