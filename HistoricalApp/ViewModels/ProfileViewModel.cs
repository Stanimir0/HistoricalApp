using HistoricalApp.Models;
using HistoricalApp.Services;
using HistoricalApp.Views;
using Microsoft.Maui.Storage;
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

        public ProfileViewModel()
        {
            _userService = new FirebaseUserService();
            LoadUserCommand = new Command(async () => await LoadCurrentUser());
            RefreshCommand = new Command(async () => await LoadCurrentUser());
            LogoutCommand = new Command(async () => await Logout());
            AdminPanelCommand = new Command(async () => await GoToAdminPanel());
            EditProfileCommand = new Command(async () => await EditProfile());
            
            // Initialize with empty user to prevent null binding crashes
            _currentUser = new User();
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
                
                // Check if user is admin
                var userRole = Preferences.Get("UserRole", string.Empty);
                var isAdmin = userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase);
                
                Console.WriteLine($"[ProfileViewModel] UserRole from Preferences: '{userRole}'");
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
                });
            }
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
            Preferences.Clear();
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async Task GoToAdminPanel()
        {
            await Shell.Current.GoToAsync("//AdminPage");
        }
    }
}
