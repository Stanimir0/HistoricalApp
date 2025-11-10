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
                if (string.IsNullOrEmpty(userId))
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
            }
        }
    }
}
