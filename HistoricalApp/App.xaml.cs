using HistoricalApp.Services;
using Microsoft.Maui.Storage;

namespace HistoricalApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await ValidateUserSession();
        }

        private async Task ValidateUserSession()
        {
            var userId = Preferences.Get("UserId", string.Empty);
            
            if (string.IsNullOrEmpty(userId))
                return; // Not logged in, no action needed

            try
            {
                var userService = new FirebaseUserService();
                var user = await userService.GetUserByIdAsync(userId);

                if (user == null)
                {
                    // User was deleted from Firebase, clear local session
                    Preferences.Clear();
                    await Shell.Current.DisplayAlert("Session Expired", 
                        "Your account data could not be found. Please log in again.", 
                        "OK");
                    await Shell.Current.GoToAsync("//LoginPage");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[App] Error validating user session: {ex.Message}");
                // Don't clear preferences on network errors, only if user truly doesn't exist
            }
        }
    }
}