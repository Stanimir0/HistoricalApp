using HistoricalApp.Models;
using HistoricalApp.Services;
using Microsoft.Maui.Storage;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly FirebaseUserService _userService;
        public ICommand LoadUserCommand { get; }

        public User CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }
        private User _currentUser;

        public ICommand RefreshCommand { get; }

        public ProfileViewModel()
        {
            _userService = new FirebaseUserService();
            LoadUserCommand = new Command(async () => await LoadCurrentUser());

            RefreshCommand = new Command(async () => await LoadCurrentUser());
            
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
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    CurrentUser = user;
                });
            }
        }
    }
}
