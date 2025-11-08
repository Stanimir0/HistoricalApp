using HistoricalApp.Models;
using HistoricalApp.Services;
using HistoricalApp.Views;
using System;
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
            GoToLoginCommand = new Command(async () =>
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage()));
        }

        private async Task OnRegister()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Please enter all fields.", "OK");
                    return;
                }

                if (Password != ConfirmPassword)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Passwords do not match.", "OK");
                    return;
                }

                var token = await _authService.RegisterUserAsync(Email, Password, UserRole.User.ToString());

                if (!string.IsNullOrEmpty(token))
                {
                    await App.Current.MainPage.DisplayAlert("✅ Success", "Account created successfully!", "OK");
                    await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Registration Failed", ex.Message, "OK");
            }
        }
    }
}
