using HistoricalApp.Helpers;

namespace HistoricalApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // RootLayout will now be found
            await AnimationHelper.SlideUpFadeIn(RootLayout);
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);

            // Your login logic here
        }

        private async void OnGoToRegisterClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);

            // Your navigation logic here
        }
    }
}
