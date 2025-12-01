using HistoricalApp.Services;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly FirebaseAuthService _authService;

        public string Email { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel()
        {
            _authService = new FirebaseAuthService();

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

            await Shell.Current.GoToAsync("//HomePage");

            await ((Shell.Current as AppShell)?.RefreshUserAccessAsync());
        }
    }
}
