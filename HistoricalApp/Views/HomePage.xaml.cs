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



        private async void OnShopClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);
            await Shell.Current.GoToAsync("//ShopPage");
        }

        private async void OnBattlesClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);
            await Shell.Current.GoToAsync("//QuizSelectionPage?category=Battles");
        }

        private async void OnEventsClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);
            await Shell.Current.GoToAsync("//QuizSelectionPage?category=Events");
        }

        private async void OnCharactersClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);
            await Shell.Current.GoToAsync("//QuizSelectionPage?category=Characters");
        }
    }
}
