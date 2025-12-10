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
            await Shell.Current.GoToAsync("//QuizSelectionPage?category=Battles");
        }

        private async void OnEventsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//QuizSelectionPage?category=Events");
        }

        private async void OnCharactersClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//QuizSelectionPage?category=Characters");
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }
    }
}
