using HistoricalApp.Models;
using HistoricalApp.Services;
using HistoricalApp.Views;
using System;
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
            GoToRegisterCommand = new Command(async () =>
                await App.Current.MainPage.Navigation.PushAsync(new RegisterPage()));
        }

        private async Task OnLogin()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Please enter both email and password.", "OK");
                    return;
                }

                var (token, roleString) = await _authService.LoginUserAsync(Email, Password);

                if (!string.IsNullOrEmpty(token))
                {
                    
                    Enum.TryParse(roleString, out UserRole role);

                    await App.Current.MainPage.DisplayAlert("✅ Login Successful", $"Role: {role}", "OK");

                    if (role == UserRole.Admin)
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new AdminPage());
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new CategorySelectionPage());
                    }
                }
                Preferences.Set("UserToken", token);
                Preferences.Set("UserEmail", Email);
                Preferences.Set("UserRole", roleString);

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Login Failed", ex.Message, "OK");
            }
        }


    }
}
