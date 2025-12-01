using HistoricalApp.Helpers;

namespace HistoricalApp.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AnimationHelper.SlideUpFadeIn(HomeRoot);
        }

        private async void OnCategoryClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);
        }

        private async void OnLeaderboardClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);
        }
    }
}
