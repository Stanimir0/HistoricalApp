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

            _ = CheckUserRoleAsync();
        }

        private async Task CheckUserRoleAsync()
        {
            try
            {
                var token = Preferences.Get("UserToken", string.Empty);

                if (string.IsNullOrEmpty(token))
                {
                    AdminPanelItem.IsVisible = false;
                    return;
                }

                var role = await _authService.GetUserRoleAsync(token);
                AdminPanelItem.IsVisible = role == "Admin";
            }
            catch
            {
                AdminPanelItem.IsVisible = false;
            }
        }
    }
}
