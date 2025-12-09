using HistoricalApp.Helpers;

namespace HistoricalApp.Controls
{
    public partial class BottomNavBar : ContentView
    {
        public BottomNavBar()
        {
            InitializeComponent();
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);

            // Use main thread to ensure safety, though usually fine via Shell
            await Shell.Current.GoToAsync("//HomePage");
        }

        private async void OnProfileClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);

            await Shell.Current.GoToAsync("//ProfilePage");
        }

        private async void OnLeaderboardClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);

            await Shell.Current.GoToAsync("//LeaderboardPage");
        }

        private async void OnAdminClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);

            await Shell.Current.GoToAsync("//AdminPage");
        }
    }
}
