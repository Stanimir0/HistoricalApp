using System;
using HistoricalApp.Views;

namespace HistoricalApp.Views
{
    public partial class CategorySelectionPage : ContentPage
    {
        public CategorySelectionPage()
        {
            InitializeComponent();
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
    }
}
