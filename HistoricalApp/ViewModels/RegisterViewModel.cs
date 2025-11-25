using HistoricalApp.Services;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly FirebaseAuthService _authService;

        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public ICommand RegisterCommand { get; }
        public ICommand GoToLoginCommand { get; }

        public RegisterViewModel()
        {
            _authService = new FirebaseAuthService();

            RegisterCommand = new Command(async () => await OnRegister());
            GoToLoginCommand = new Command(async () => await Shell.Current.GoToAsync("//LoginPage"));
        }

        private async Task OnRegister()
        {
            if (string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await App.Current.MainPage.DisplayAlert("Error", "All fields are required.", "OK");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            // Firebase returns string for success, so check if it's null / empty
            var result = await _authService.RegisterUserAsync(Email, Password);

            if (string.IsNullOrEmpty(result))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Registration failed.", "OK");
                return;
            }

            await App.Current.MainPage.DisplayAlert("Success", "Account created!", "OK");

            await Shell.Current.GoToAsync("//HomePage");

            (Shell.Current as AppShell)?.RefreshUserAccessAsync();
        }
    }
}
