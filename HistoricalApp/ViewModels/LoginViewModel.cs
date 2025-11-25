using HistoricalApp.Services;
using System.Windows.Input;
using Microsoft.Maui.Storage;

namespace HistoricalApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly FirebaseAuthService _authService;

        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public string Email { get; set; }
        public string Password { get; set; }

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
                await App.Current.MainPage.DisplayAlert("Error", "Please enter both email and password.", "OK");
                return;
            }

            try
            {
                var userId = await _authService.LoginUserAsync(Email, Password);

                if (string.IsNullOrEmpty(userId))
                {
                    await App.Current.MainPage.DisplayAlert("Login Failed", "Invalid email or password.", "OK");
                    return;
                }

                await App.Current.MainPage.DisplayAlert("Success", "Logged in successfully!", "OK");

                await Shell.Current.GoToAsync("//HomePage");

                (Shell.Current as AppShell)?.RefreshUserAccessAsync();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
