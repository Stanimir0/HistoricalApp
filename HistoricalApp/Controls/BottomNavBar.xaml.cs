using HistoricalApp.Helpers;

namespace HistoricalApp.Controls
{
    public partial class BottomNavBar : ContentView
    {
        public BottomNavBar()
        {
            InitializeComponent();
        }

        private async void OnHomeTapped(object sender, TappedEventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);

            await Shell.Current.GoToAsync("//HomePage");
        }

        private async void OnProfileTapped(object sender, TappedEventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);

            await Shell.Current.GoToAsync("//ProfilePage");
        }

        private async void OnLeaderboardTapped(object sender, TappedEventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);

            await Shell.Current.GoToAsync("//LeaderboardPage");
        }

        private async void OnAdminTapped(object sender, TappedEventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);

            await Shell.Current.GoToAsync("//AdminPage");
        }
    }
}
