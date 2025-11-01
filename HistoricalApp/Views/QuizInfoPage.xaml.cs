using HistoricalApp.Models;
using System.Windows.Input;

namespace HistoricalApp.Views
{
    public partial class QuizInfoPage : ContentPage
    {
        public Quiz Quiz { get; }
        public ICommand StartQuizCommand { get; }

        public QuizInfoPage(Quiz quiz)
        {
            InitializeComponent();

            Quiz = quiz;
            StartQuizCommand = new Command(OnStartQuiz);

            BindingContext = Quiz;
        }

        private async void OnStartQuiz()
        {
            await DisplayAlert("Quiz Starting", $"Good luck with '{Quiz.Title}'!", "OK");

          
        }
    }
}
