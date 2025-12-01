using HistoricalApp.ViewModels;
using HistoricalApp.Helpers;

namespace HistoricalApp.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AnimationHelper.SlideUpFadeIn(RootLayout);
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);
        }

        private async void OnBackToLoginClicked(object sender, EventArgs e)
        {
            if (sender is View v)
                await AnimationHelper.AnimateButtonPress(v);
        }
    }
}
