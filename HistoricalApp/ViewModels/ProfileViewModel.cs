using HistoricalApp.Helpers;
using HistoricalApp.Models;
using HistoricalApp.Services;
using HistoricalApp.Views;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly FirebaseUserService _userService;
        public ICommand LoadUserCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand AdminPanelCommand { get; }
        public ICommand EditProfileCommand { get; }
        public ICommand GiftCurrencyCommand { get; }

        // Translation service for live language updates
        public TranslationService Translations => TranslationService.Instance;

        public User CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }
        private User _currentUser;

        public bool IsAdmin
        {
            get => _isAdmin;
            set => SetProperty(ref _isAdmin, value);
        }
        private bool _isAdmin;

        public ICommand RefreshCommand { get; }
        public ICommand ChangeLanguageCommand { get; }

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (SetProperty(ref _selectedLanguage, value))
                {
                    ChangeLanguageCommand.Execute(value);
                }
            }
        }
        private string _selectedLanguage = "English";

        public string BadgeEmoji
        {
            get => _badgeEmoji;
            set => SetProperty(ref _badgeEmoji, value);
        }
        private string _badgeEmoji = string.Empty;

        public bool HasBadge => !string.IsNullOrEmpty(BadgeEmoji);

        public Color ProfileBorderColor
        {
            get => _profileBorderColor;
            set => SetProperty(ref _profileBorderColor, value);
        }
        private Color _profileBorderColor = Color.FromArgb("#FFD700");

        public int ProfileBorderWidth
        {
            get => _profileBorderWidth;
            set => SetProperty(ref _profileBorderWidth, value);
        }
        private int _profileBorderWidth = 1;

        // === Level Display ===
        public string LevelText => CurrentUser != null ? $"Level {CurrentUser.Level}" : "Level 1";
        public double LevelProgress => CurrentUser != null
            ? LevelService.GetLevelProgress(CurrentUser.TotalXP, CurrentUser.Level)
            : 0.0;
        public string LevelProgressText => CurrentUser != null
            ? $"{CurrentUser.TotalXP}/{LevelService.GetXPForNextLevel(CurrentUser.Level)} XP"
            : "0/100 XP";

        // === Streak Display ===
        public string StreakText => CurrentUser != null && CurrentUser.Streak > 0
            ? $"{StreakService.GetStreakEmoji(CurrentUser.Streak)} {CurrentUser.Streak} day streak"
            : "No streak yet";
        public bool HasStreak => CurrentUser != null && CurrentUser.Streak > 0;

        // === Secret Badges ===
        public ObservableCollection<SecretBadgeDisplay> SecretBadges { get; } = new();
        public bool HasSecretBadges => SecretBadges.Count > 0;

        // === Daily Missions ===
        public ObservableCollection<DailyMission> DailyMissions { get; } = new();
        public bool HasMissions => DailyMissions.Count > 0;

        public ProfileViewModel()
        {
            _userService = new FirebaseUserService();
            LoadUserCommand = new Command(async () => await LoadCurrentUser());
            RefreshCommand = new Command(async () => await LoadCurrentUser());
            LogoutCommand = new Command(async () => await Logout());
            AdminPanelCommand = new Command(async () => await GoToAdminPanel());
            EditProfileCommand = new Command(async () => await EditProfile());
            ChangeLanguageCommand = new Command<string>(ChangeLanguage);
            GiftCurrencyCommand = new Command(async () => await GiftCurrency());
            
            // Initialize with empty user to prevent null binding crashes
            _currentUser = new User();
            
            // Load current language preference
            var currentLang = LocalizationHelper.GetCurrentLanguage();
            _selectedLanguage = currentLang == "bg" ? "Български" : "English";
        }

        public async Task LoadCurrentUser()
        {
            var userId = Preferences.Get("UserId", string.Empty);

            if (string.IsNullOrEmpty(userId))
                return;

            var user = await _userService.GetUserByIdAsync(userId);

            if (user != null)
            {
                user.RecalculateRank();
                
                // IMPORTANT: Use role from DATABASE, not Preferences
                // This ensures admin panel appears if role was changed in Firebase
                var isAdmin = user.Role?.Equals("Admin", StringComparison.OrdinalIgnoreCase) ?? false;
                
                // Update Preferences to stay in sync
                if (!string.IsNullOrEmpty(user.Role))
                {
                    Preferences.Set("UserRole", user.Role);
                }
                
                Console.WriteLine($"[ProfileViewModel] User.Role from DB: '{user.Role}'");
                Console.WriteLine($"[ProfileViewModel] IsAdmin calculated: {isAdmin}");
                
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    CurrentUser = user;
                    IsAdmin = isAdmin;
                    
                    if (!string.IsNullOrEmpty(user.ProfileImage))
                    {
                        var imageBytes = Convert.FromBase64String(user.ProfileImage);
                        UserProfileImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                    }
                    else
                    {
                        UserProfileImageSource = "dotnet_bot.png"; // Default image
                    }

                    Console.WriteLine($"[ProfileViewModel] IsAdmin property set to: {IsAdmin}");
                    
                    // Load equipped badge
                    LoadEquippedBadge();
                    // Load equipped border
                    LoadEquippedBorder();
                    // Load level/streak info
                    OnPropertyChanged(nameof(LevelText));
                    OnPropertyChanged(nameof(LevelProgress));
                    OnPropertyChanged(nameof(LevelProgressText));
                    OnPropertyChanged(nameof(StreakText));
                    OnPropertyChanged(nameof(HasStreak));
                    // Load secret badges
                    LoadSecretBadges();
                    // Load daily missions
                    LoadDailyMissions();
                });
            }
        }

        private void LoadEquippedBadge()
        {
            if (!string.IsNullOrEmpty(CurrentUser.EquippedBadge))
            {
                var allItems = ShopService.GetShopItems();
                var badgeItem = allItems.FirstOrDefault(i => i.Id == CurrentUser.EquippedBadge);
                if (badgeItem != null)
                {
                    BadgeEmoji = badgeItem.IconEmoji;
                    OnPropertyChanged(nameof(HasBadge));
                }
            }
            else
            {
                BadgeEmoji = string.Empty;
                OnPropertyChanged(nameof(HasBadge));
            }
        }

        private void LoadEquippedBorder()
        {
            if (!string.IsNullOrEmpty(CurrentUser.EquippedBorder))
            {
                var allItems = ShopService.GetShopItems();
                var borderItem = allItems.FirstOrDefault(i => i.Id == CurrentUser.EquippedBorder);
                if (borderItem != null)
                {
                    // Map border to color
                    ProfileBorderColor = GetBorderColor(borderItem.Id);
                    ProfileBorderWidth = 4;
                }
                else
                {
                    ProfileBorderColor = Color.FromArgb("#FFD700");
                    ProfileBorderWidth = 1;
                }
            }
            else
            {
                ProfileBorderColor = Color.FromArgb("#FFD700");
                ProfileBorderWidth = 1;
            }
        }

        private Color GetBorderColor(string borderId)
        {
            return borderId switch
            {
                "border_simple" => Color.FromArgb("#CCCCCC"),
                "border_gold" => Color.FromArgb("#FFD700"),
                "border_ancient" => Color.FromArgb("#CD853F"),
                "border_ice" => Color.FromArgb("#87CEEB"),
                "border_fire" => Color.FromArgb("#FF4500"),
                "border_royal" => Color.FromArgb("#9C27B0"),
                "border_diamond" => Color.FromArgb("#B9F2FF"),
                _ => Color.FromArgb("#FFD700"),
            };
        }

        private void LoadSecretBadges()
        {
            SecretBadges.Clear();
            if (CurrentUser?.SecretBadges == null) return;

            foreach (var badgeId in CurrentUser.SecretBadges)
            {
                var badge = SecretBadgeService.GetBadgeById(badgeId);
                if (badge != null)
                {
                    SecretBadges.Add(new SecretBadgeDisplay
                    {
                        Name = badge.Name,
                        Emoji = badge.Emoji
                    });
                }
            }
            OnPropertyChanged(nameof(HasSecretBadges));
        }

        private void LoadDailyMissions()
        {
            DailyMissions.Clear();
            if (CurrentUser == null) return;

            var missions = DailyMissionService.GetDailyMissions(CurrentUser);
            foreach (var mission in missions)
            {
                DailyMissions.Add(mission);
            }
            OnPropertyChanged(nameof(HasMissions));
        }

        private async Task GiftCurrency()
        {
            string recipient = await Application.Current.MainPage.DisplayPromptAsync(
                "Gift Coins",
                "Enter the username of the friend:",
                "Send",
                "Cancel",
                placeholder: "username");

            if (string.IsNullOrWhiteSpace(recipient)) return;

            string amountStr = await Application.Current.MainPage.DisplayPromptAsync(
                "Gift Coins",
                $"How many coins to send to {recipient}?",
                "Send",
                "Cancel",
                keyboard: Microsoft.Maui.Keyboard.Numeric);

            if (string.IsNullOrWhiteSpace(amountStr) || !int.TryParse(amountStr, out int amount) || amount <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a valid amount.", "OK");
                return;
            }

            if (CurrentUser.Currency < amount)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You don't have enough coins.", "OK");
                return;
            }

            // Find recipient by username
            var recipientUser = await _userService.GetUserByUsernameAsync(recipient);
            if (recipientUser == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"User '{recipient}' not found.", "OK");
                return;
            }

            if (recipientUser.Id == CurrentUser.Id)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You can't gift coins to yourself.", "OK");
                return;
            }

            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirm Gift",
                $"Send {amount} coins to {recipientUser.UserName}?",
                "Yes", "No");

            if (!confirm) return;

            // Process transfer
            CurrentUser.Currency -= amount;
            recipientUser.Currency += amount;

            await _userService.UpdateUserAsync(CurrentUser.Id, CurrentUser);
            await _userService.UpdateUserAsync(recipientUser.Id, recipientUser);

            OnPropertyChanged(nameof(CurrentUser));

            await Application.Current.MainPage.DisplayAlert(
                "Success",
                $"You sent {amount} coins to {recipientUser.UserName}!",
                "OK");
        }

        private async Task EditProfile()
        {
            if (CurrentUser != null && !string.IsNullOrWhiteSpace(CurrentUser.Id))
            {
                await Shell.Current.Navigation.PushAsync(new EditProfilePage(CurrentUser));
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Please wait for your profile to load before editing.", "OK");
            }
        }

        public ImageSource UserProfileImageSource
        {
            get => _userProfileImageSource;
            set => SetProperty(ref _userProfileImageSource, value);
        }
        private ImageSource _userProfileImageSource;

        private async Task Logout()
        {
            try
            {
                // Navigate FIRST, then clear — prevents HomePage OnAppearing 
                // from firing with cleared preferences and crashing
                await Shell.Current.GoToAsync("//LoginPage");
                Preferences.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProfileViewModel] Logout error: {ex.Message}");
                // Fallback: clear anyway
                Preferences.Clear();
            }
        }

        private async Task GoToAdminPanel()
        {
            await Shell.Current.GoToAsync("//AdminPage");
        }

        private void ChangeLanguage(string language)
        {
            var languageCode = language == "Български" ? "bg" : "en";
            LocalizationHelper.SetLanguage(languageCode);
            
            // Update translation service to refresh UI immediately
            TranslationService.Instance.SetLanguage(languageCode);
            
            // Notify UI is updated
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.DisplayAlert(
                    languageCode == "bg" ? "Езикът е променен" : "Language Changed",
                    languageCode == "bg" ? "UI е актуализиран!" : "UI has been updated!",
                    "OK");
            });
        }
    }
}
