using HistoricalApp.Helpers;
using Microsoft.Maui.Storage;

namespace HistoricalApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                // If already logged in, skip to HomePage
                var userId = Preferences.Get("UserId", string.Empty);
                if (!string.IsNullOrEmpty(userId))
                {
                    // Small delay to ensure Shell is fully ready
                    await Task.Delay(100);
                    await Shell.Current.GoToAsync("//HomePage");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LoginPage] Auto-redirect error: {ex.Message}");
            }

            // Show login form with animation
            try
            {
                await AnimationHelper.SlideUpFadeIn(RootLayout);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LoginPage] Animation error: {ex.Message}");
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);

            // Your login logic here
        }

        private async void OnGoToRegisterClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);

            // Your navigation logic here
        }
    }
}
