using HistoricalApp.Services;
using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace HistoricalApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
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

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            _authService = new FirebaseAuthService();
            RegisterCommand = new Command(async () => await OnRegister());
        }

        private async Task OnRegister()
        {
            try
            {
                var userId = await _authService.RegisterUserAsync(Email, Password);
                await App.Current.MainPage.DisplayAlert("Registration Successful", $"Your account has been created! User ID: {userId}", "OK");
                await Shell.Current.GoToAsync("//ProfilePage");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Registration Failed", ex.Message, "OK");
            }
        }
    }
}
