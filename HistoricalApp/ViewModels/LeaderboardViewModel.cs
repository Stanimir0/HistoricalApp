using HistoricalApp.Models;
using HistoricalApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class LeaderboardItem
    {
        public User User { get; set; }
        public int Position { get; set; }
        public Color RankColor { get; set; }
        public bool IsTopThree => Position <= 3;
    }

    public class LeaderboardViewModel : BaseViewModel
    {
        private readonly FirebaseUserService _userService;

        public ObservableCollection<LeaderboardItem> Users { get; set; } = new();

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
                int rank = 1;
                foreach (var user in sortedUsers)
                {
                    Color rankColor = Colors.White; // Default
                    if (rank == 1) rankColor = Color.FromArgb("#FFD700"); // Gold
                    else if (rank == 2) rankColor = Color.FromArgb("#C0C0C0"); // Silver
                    else if (rank == 3) rankColor = Color.FromArgb("#CD7F32"); // Bronze

                    Users.Add(new LeaderboardItem 
                    { 
                        User = user, 
                        Position = rank,
                        RankColor = rankColor
                    });
                    rank++;
                }
            });
        }
    }
}
