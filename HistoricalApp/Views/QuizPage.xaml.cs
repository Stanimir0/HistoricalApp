using HistoricalApp.ViewModels;
using HistoricalApp.Helpers;
using HistoricalApp.Models;

namespace HistoricalApp.Views
{
    public partial class QuizPage : ContentPage
    {
        public QuizPage(Quiz quiz)
        {
            InitializeComponent();

            var viewModel = new QuizPlayViewModel();
            BindingContext = viewModel;

            // Use async load to get hint inventory
            _ = viewModel.LoadQuizAsync(quiz);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AnimationHelper.SlideUpFadeIn(RootLayout);
        }

        private async void OnAnswerTapped(object sender, TappedEventArgs e)
        {
            if (sender is Border border)
            {
                // Quick press animation
                await AnimationHelper.AnimateButtonPress(border);
            }
        }
    }
}
