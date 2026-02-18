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
            // Check and reset leaderboard periods if needed
            _ = CheckLeaderboardResets();
        }

        private async Task CheckLeaderboardResets()
        {
            try
            {
                var resetService = new Helpers.LeaderboardResetService();
                await resetService.CheckAndResetPeriodsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[App] Error checking leaderboard resets: {ex.Message}");
            }
        }
    }
}