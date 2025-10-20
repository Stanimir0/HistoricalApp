using HistoricalApp.Services;
using System;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly FirebaseAuthService _authService;
        private string email;
        private string password;

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            _authService = new FirebaseAuthService();
            LoginCommand = new Command(async () => await OnLogin());
            RegisterCommand = new Command(async () => await OnRegister());
        }

        private async Task OnLogin()
        {
            try
            {
                var response = await _authService.LoginUserAsync(Email, Password);
                await App.Current.MainPage.DisplayAlert("Success", "Logged in successfully!", "OK");

                
                await App.Current.MainPage.Navigation.PushAsync(new Views.QuizSelectionPage());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task OnRegister()
        {
            try
            {
                var response = await _authService.RegisterUserAsync(Email, Password);
                await App.Current.MainPage.DisplayAlert("Success", "Account created successfully!", "OK");
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
