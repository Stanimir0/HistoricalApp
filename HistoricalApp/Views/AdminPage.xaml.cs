using HistoricalApp.Services;
using Microsoft.Maui.Storage;

namespace HistoricalApp.Views
{
    public partial class AdminPage : ContentPage
    {
        private readonly FirebaseAuthService _authService;

        public AdminPage()
        {
            InitializeComponent();
            _authService = new FirebaseAuthService();

            // Check access only when page becomes visible
            this.Appearing += AdminPage_Appearing;
        }

        private async void AdminPage_Appearing(object sender, EventArgs e)
        {
            await VerifyAccessAsync();
        }

        private async Task VerifyAccessAsync()
        {
            var userId = Preferences.Get("UserId", string.Empty);
            if (string.IsNullOrEmpty(userId))
            {
                await DisplayAlert("Access Denied", "You are not logged in.", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
                return;
            }

            var role = await _authService.GetUserRoleAsync(userId);
            if (!role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                await DisplayAlert("Access Denied", "Only admins can access this page.", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }
    }
}
