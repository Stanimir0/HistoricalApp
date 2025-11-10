using HistoricalApp.Services;
using HistoricalApp.Views; 
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace HistoricalApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly FirebaseAuthService _authService;

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel()
        {
            _authService = new FirebaseAuthService();

            LoginCommand = new Command(async () => await OnLogin());
            GoToRegisterCommand = new Command(async () => await OnGoToRegister());
        }

        private async Task OnLogin()
        {
            try
            {
                var userId = await _authService.LoginUserAsync(Email, Password);
                await App.Current.MainPage.DisplayAlert("Success", $"Welcome back! User ID: {userId}", "OK");

                await App.Current.MainPage.Navigation.PushAsync(new ProfilePage());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Login Failed", ex.Message, "OK");
            }
        }

        private async Task OnGoToRegister()
        {
            
            await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}
