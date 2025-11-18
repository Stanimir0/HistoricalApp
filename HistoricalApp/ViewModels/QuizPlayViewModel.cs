using Firebase.Database;
using Firebase.Database.Query;
using HistoricalApp.Models;
using Microsoft.Maui.Storage;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class QuizPlayViewModel : BaseViewModel
    {
        private readonly FirebaseClient _client;
        private readonly string _dbUrl = "https://historical-f19c6-default-rtdb.europe-west1.firebasedatabase.app/";

        public Quiz CurrentQuiz { get; private set; }
        public string CurrentQuizTitle => CurrentQuiz?.Title ?? "";

        private Question _currentQuestion;
        public Question CurrentQuestion
        {
            get => _currentQuestion;
            set => SetProperty(ref _currentQuestion, value);
        }

        private int _currentIndex = 0;
        private int _score = 0;

        public ICommand SelectAnswerCommand { get; }
        public ICommand NextQuestionCommand { get; }

        public bool IsNextButtonVisible => _currentIndex < (CurrentQuiz?.Questions.Count ?? 0) - 1;

        public QuizPlayViewModel()
        {
            _client = new FirebaseClient(_dbUrl);
            SelectAnswerCommand = new Command<string>(OnAnswerSelected);
            NextQuestionCommand = new Command(OnNextQuestion);
        }

        public void LoadQuiz(Quiz quiz)
        {
            CurrentQuiz = quiz;
            _currentIndex = 0;
            _score = 0;

            if (quiz.Questions?.Count > 0)
                CurrentQuestion = quiz.Questions[_currentIndex];
        }

        private async void OnAnswerSelected(string selected)
        {
            var correct = CurrentQuestion.Answers[CurrentQuestion.CorrectAnswerIndex];

            if (selected == correct)
                _score += CurrentQuiz.Points;

            if (_currentIndex < CurrentQuiz.Questions.Count - 1)
            {
                _currentIndex++;
                CurrentQuestion = CurrentQuiz.Questions[_currentIndex];
                OnPropertyChanged(nameof(IsNextButtonVisible));
            }
            else
            {
                await SaveUserProgressAsync();
                await App.Current.MainPage.DisplayAlert(
                    "Quiz Complete",
                    $"You earned {_score} points!",
                    "OK"
                );

                await App.Current.MainPage.Navigation.PopAsync();
            }
        }

        private void OnNextQuestion()
        {
            if (_currentIndex < CurrentQuiz.Questions.Count - 1)
            {
                _currentIndex++;
                CurrentQuestion = CurrentQuiz.Questions[_currentIndex];
                OnPropertyChanged(nameof(IsNextButtonVisible));
            }
        }

        private async Task SaveUserProgressAsync()
        {
            var userId = Preferences.Get("UserId", string.Empty);

            if (string.IsNullOrEmpty(userId))
            {
                await App.Current.MainPage.DisplayAlert("Error", "User ID not found.", "OK");
                return;
            }

            User user;

            try
            {
                user = await _client.Child("users").Child(userId).OnceSingleAsync<User>();
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to load user.", "OK");
                return;
            }

            if (user == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "User profile missing.", "OK");
                return;
            }

            user.TotalPoints += _score;
            user.RecalculateRank();

            try
            {
                await _client.Child("users").Child(userId).PutAsync(user);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error Saving Progress", "Could not update Firebase.", "OK");
            }
        }
    }
}
