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
            viewModel.LoadQuiz(quiz);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AnimationHelper.SlideUpFadeIn(RootLayout);
        }

        
        private async void OnAnswerTapped(object sender, TappedEventArgs e)
        {
            if (sender is Frame frame)
            {
              
                var chosen = frame.BindingContext as string;

                if (BindingContext is QuizPlayViewModel vm)
                {
                    bool isCorrect = vm.IsCorrectAnswer(chosen);

                    if (isCorrect)
                        await AnimationHelper.GlowCorrect(frame);
                    else
                        await AnimationHelper.Shake(frame);
                }
            }
        }
    }
}
