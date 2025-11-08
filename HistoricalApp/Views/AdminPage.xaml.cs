using HistoricalApp.Services;
using Microsoft.Maui.Storage;
using System;

namespace HistoricalApp.Views
{
    public partial class AdminPage : ContentPage
    {
        private readonly FirebaseAuthService _authService;

        public AdminPage()
        {
            InitializeComponent();
            _authService = new FirebaseAuthService();
            _ = CheckAccessAsync();
        }

        private async Task CheckAccessAsync()
        {
            try
            {
                var token = Preferences.Get("UserToken", string.Empty);

                if (string.IsNullOrEmpty(token))
                {
                    await DisplayAlert("Access Denied", "You are not logged in.", "OK");
                    await Navigation.PushAsync(new LoginPage());
                    return;
                }

                var role = await _authService.GetUserRoleAsync(token);
                Console.WriteLine($"[DEBUG] User role: {role}");

                if (!string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
                {
                    await DisplayAlert("Access Denied", "Only admins can access this panel.", "OK");
                    await Navigation.PushAsync(new CategorySelectionPage());
                    return;
                }

                Console.WriteLine("[DEBUG] Access granted to admin panel.");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to verify admin access: {ex.Message}", "OK");
                await Navigation.PushAsync(new CategorySelectionPage());
            }
        }
    }
}
