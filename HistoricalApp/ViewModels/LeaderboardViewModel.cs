using HistoricalApp.Models;
using HistoricalApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class LeaderboardViewModel : BaseViewModel
    {
        private readonly FirebaseUserService _userService;

        public ObservableCollection<User> Users { get; set; } = new();

        public ICommand LoadLeaderboardCommand { get; }

        public LeaderboardViewModel()
        {
            _userService = new FirebaseUserService();
            LoadLeaderboardCommand = new Command(async () => await LoadLeaderboard());
        }

        public async Task LoadLeaderboard()
        {
            var allUsers = await _userService.GetAllUsersAsync();

            if (allUsers == null || allUsers.Count == 0)
                return;

            // Recalculate ranks (if points changed)
            foreach (var user in allUsers)
                user.RecalculateRank();

            var sortedUsers = allUsers
                .OrderByDescending(u => u.TotalPoints)
                .ToList();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Users.Clear();
                foreach (var user in sortedUsers)
                    Users.Add(user);
            });
        }
    }
}
