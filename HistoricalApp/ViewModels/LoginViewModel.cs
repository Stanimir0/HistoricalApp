using HistoricalApp.Helpers;
using HistoricalApp.Services;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly FirebaseAuthService _authService;
        private readonly FirebaseUserService _userService;

        public string Email { get; set; }
        public string Password { get; set; }

        // Translation service for live language updates
        public TranslationService Translations => TranslationService.Instance;

        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel()
        {
            _authService = new FirebaseAuthService();
            _userService = new FirebaseUserService();

            LoginCommand = new Command(async () => await OnLogin());
            GoToRegisterCommand = new Command(async () => await Shell.Current.GoToAsync("//RegisterPage"));
        }

        private async Task OnLogin()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Email and password are required.", "OK");
                return;
            }

            var userId = await _authService.LoginUserAsync(Email, Password);

            if (string.IsNullOrEmpty(userId))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Invalid email or password.", "OK");
                return;
            }

            // Save user ID on device
            Preferences.Set("UserId", userId);

            // Fetch user data to get role
            var user = await _userService.GetUserByIdAsync(userId);
            if (user != null && !string.IsNullOrEmpty(user.Role))
            {
                Preferences.Set("UserRole", user.Role);
            }

            await Shell.Current.GoToAsync("//HomePage");

            await ((Shell.Current as AppShell)?.RefreshUserAccessAsync());
        }
    }
}
