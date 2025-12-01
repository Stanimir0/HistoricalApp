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

            // Optional: explicit route registrations (extra safety)
            Routing.RegisterRoute("HomePage", typeof(HomePage));
            Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));
            Routing.RegisterRoute("LeaderboardPage", typeof(LeaderboardPage));
            Routing.RegisterRoute("AdminPage", typeof(AdminPage));

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
        }
    }
}
