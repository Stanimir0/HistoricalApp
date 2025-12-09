using HistoricalApp.Helpers;
using HistoricalApp.ViewModels;

namespace HistoricalApp.Views
{
    public partial class LeaderboardPage : ContentPage
    {
        public LeaderboardPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AnimationHelper.SlideUpFadeIn(RootLayout);

            if (BindingContext is LeaderboardViewModel vm)
                vm.LoadLeaderboardCommand.Execute(null);
        }
    }
}
