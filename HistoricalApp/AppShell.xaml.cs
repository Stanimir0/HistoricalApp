using HistoricalApp.Services;
using Microsoft.Maui.Storage;

namespace HistoricalApp
{
    public partial class AppShell : Shell
    {
        private readonly FirebaseAuthService _authService;

        public AppShell()
        {
            InitializeComponent();
            _authService = new FirebaseAuthService();

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(800);
                await RefreshUserAccessAsync();
            });
        }

        public async Task RefreshUserAccessAsync()
        {
            try
            {
                var userId = Preferences.Get("UserId", string.Empty);
                bool isLoggedIn = !string.IsNullOrEmpty(userId);

                LoginItem.IsVisible = !isLoggedIn;

                if (!isLoggedIn)
                {
                    AdminPanelItem.IsVisible = false;
                    return;
                }

                var role = await _authService.GetUserRoleAsync(userId);
                AdminPanelItem.IsVisible = role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                AdminPanelItem.IsVisible = false;
                LoginItem.IsVisible = true;
            }
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            Preferences.Remove("UserId");
            Preferences.Remove("Email");
            Preferences.Remove("IdToken");

            AdminPanelItem.IsVisible = false;
            LoginItem.IsVisible = true;

            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
