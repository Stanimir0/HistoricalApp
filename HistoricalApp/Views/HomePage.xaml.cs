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

            // Load user data from Firebase so level/streak/coins display correctly
            if (BindingContext is HistoricalApp.ViewModels.ProfileViewModel vm)
            {
                await vm.LoadCurrentUser();
            }

            await AnimationHelper.SlideUpFadeIn(HomeRoot);
        }

        private async void OnShopClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);
            await Shell.Current.GoToAsync("//ShopPage");
        }

        private async void OnPlayQuizClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);
            await Shell.Current.GoToAsync("//CategorySelectionPage");
        }
    }
}
