using HistoricalApp.Services;
using HistoricalApp.Views;
using Microsoft.Maui.Storage;

namespace HistoricalApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Only register routes not defined as ShellContent in XAML
            Routing.RegisterRoute("EditProfilePage", typeof(EditProfilePage));
        }

        public async Task RefreshUserAccessAsync()
        {
            var userId = Preferences.Get("UserId", string.Empty);
            if (string.IsNullOrEmpty(userId))
            {
                Preferences.Set("IsAdmin", false);
                return;
            }

            var userService = new FirebaseUserService();
            var user = await userService.GetUserByIdAsync(userId);

            bool isAdmin = user?.Role == "Admin";
            Preferences.Set("IsAdmin", isAdmin);

            // Apply equipped theme
            if (user != null && !string.IsNullOrEmpty(user.EquippedTheme))
            {
                ThemeService.Instance.ApplyTheme(user.EquippedTheme);
            }
            else
            {
                ThemeService.Instance.ApplyTheme(string.Empty); // Default
            }
        }
    }
}
