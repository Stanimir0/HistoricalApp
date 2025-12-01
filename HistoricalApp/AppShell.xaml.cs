using HistoricalApp.Services;
using HistoricalApp.Views;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace HistoricalApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        public async Task RefreshUserAccessAsync()
        {
            var userId = Preferences.Get("UserId", string.Empty);
            if (string.IsNullOrEmpty(userId))
                return;

            var userService = new FirebaseUserService();
            var user = await userService.GetUserByIdAsync(userId);

            if (user == null)
                return;

            //// Add admin tab only if user is Admin
            //if (user.Role == "Admin")
            //{
            //    bool exists = MainTabBar.Items.Any(x => x.Route == "AdminPage");

            //    if (!exists)
            //    {
            //        var adminTab = new ShellContent()
            //        {
            //            Title = "Admin",
            //            Route = "AdminPage",
            //            Icon = "icon_admin.png",
            //            ContentTemplate = new DataTemplate(typeof(AdminPage))
            //        };

            //        MainTabBar.Items.Add(adminTab);
            //    }
            //}
        }
    }
}
