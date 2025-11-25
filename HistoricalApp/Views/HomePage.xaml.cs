using HistoricalApp.ViewModels;

namespace HistoricalApp.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ProfileViewModel vm)
                vm.LoadUserCommand.Execute(null);
        }

        private async void OnBattlesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QuizSelectionPage("Battles"));
        }

        private async void OnEventsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QuizSelectionPage("Events"));
        }

        private async void OnCharactersClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QuizSelectionPage("Characters"));
        }

        private async void OnLeaderboardClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LeaderboardPage());
        }
    }
}
