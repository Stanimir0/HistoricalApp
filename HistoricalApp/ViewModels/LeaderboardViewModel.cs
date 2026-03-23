using HistoricalApp.Helpers;
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
        public int PotentialReward { get; set; }
        public string BadgeEmoji { get; set; } = string.Empty;
        public bool HasBadge => !string.IsNullOrEmpty(BadgeEmoji);

        public ImageSource ProfileImageSource
        {
            get
            {
                if (User != null && !string.IsNullOrEmpty(User.ProfileImage))
                {
                    try
                    {
                        var bytes = Convert.FromBase64String(User.ProfileImage);
                        return ImageSource.FromStream(() => new MemoryStream(bytes));
                    }
                    catch { }
                }
                return "dotnet_bot.png";
            }
        }
    }

    public class LeaderboardViewModel : BaseViewModel
    {
        private readonly FirebaseUserService _userService;
        private readonly LeaderboardResetService _resetService;
        private string _selectedPeriod = "Daily";

        public ObservableCollection<LeaderboardItem> Users { get; set; } = new();

        public string SelectedPeriod
        {
            get => _selectedPeriod;
            set
            {
                _selectedPeriod = value;
                OnPropertyChanged();
                _ = LoadLeaderboard();
            }
        }

        // Translation service for live language updates
        public TranslationService Translations => TranslationService.Instance;

        public ICommand LoadLeaderboardCommand { get; }
        public ICommand SwitchPeriodCommand { get; }

        public LeaderboardViewModel()
        {
            _userService = new FirebaseUserService();
            _resetService = new LeaderboardResetService();
            LoadLeaderboardCommand = new Command(async () => await LoadLeaderboard());
            SwitchPeriodCommand = new Command<string>(async (period) => await SwitchPeriod(period));
        }

        private async Task SwitchPeriod(string period)
        {
            SelectedPeriod = period;
        }

        public async Task LoadLeaderboard()
        {
            IsLoading = true;
            // Check if any periods need to be reset first
            await _resetService.CheckAndResetPeriodsAsync();

            var sortedUsers = await _userService.GetLeaderboardByPeriodAsync(SelectedPeriod);

            if (sortedUsers == null || sortedUsers.Count == 0)
                return;

            // Recalculate ranks (if points changed)
            foreach (var user in sortedUsers)
                user.RecalculateRank();

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

                    // Calculate potential reward based on period and position
                    int reward = CalculateReward(rank, SelectedPeriod);

                    // Build badge emoji (shop badge + secret badge)
                    string badgeEmoji = "";
                    if (!string.IsNullOrEmpty(user.EquippedBadge))
                    {
                        var shopBadge = ShopService.GetShopItems().FirstOrDefault(i => i.Id == user.EquippedBadge);
                        if (shopBadge != null) badgeEmoji += shopBadge.IconEmoji;
                    }
                    if (!string.IsNullOrEmpty(user.EquippedSecretBadge))
                    {
                        var secretBadge = SecretBadgeService.GetBadgeById(user.EquippedSecretBadge);
                        if (secretBadge != null) badgeEmoji += secretBadge.Emoji;
                    }

                    Users.Add(new LeaderboardItem 
                    { 
                        User = user, 
                        Position = rank,
                        RankColor = rankColor,
                        PotentialReward = reward,
                        BadgeEmoji = badgeEmoji
                    });
                    rank++;
                }
            });
            IsLoading = false;
        }

        private int CalculateReward(int position, string period)
        {
            return period.ToLower() switch
            {
                "daily" => position switch
                {
                    1 => 100,
                    2 => 75,
                    3 => 50,
                    >= 4 and <= 10 => 25,
                    _ => 0
                },
                "weekly" => position switch
                {
                    1 => 500,
                    2 => 350,
                    3 => 250,
                    >= 4 and <= 10 => 100,
                    _ => 0
                },
                "monthly" => position switch
                {
                    1 => 2000,
                    2 => 1500,
                    3 => 1000,
                    >= 4 and <= 10 => 500,
                    _ => 0
                },
                _ => 0
            };
        }
    }
}
