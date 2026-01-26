using HistoricalApp.Helpers;
using HistoricalApp.Models;
using HistoricalApp.Services;
using HistoricalApp.Views;
using Microsoft.Maui.Storage;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class QuizPlayViewModel : BaseViewModel
    {
        private readonly FirebaseQuizService _quizService;
        private readonly FirebaseUserService _userService;

        public ICommand SelectAnswerCommand { get; }
        public ICommand NextQuestionCommand { get; }

        public Quiz CurrentQuiz { get; private set; }

        public TranslationService Translations => TranslationService.Instance;

        private Question _currentQuestion;
        public Question CurrentQuestion
        {
            get => _currentQuestion;
            set => SetProperty(ref _currentQuestion, value);
        }

        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (SetProperty(ref _currentIndex, value))
                    UpdateQuestionUI();
            }
        }

        private int _score;
        public int Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
        }

        public string CurrentQuizTitle { get; set; }

        public string QuestionProgress =>
            CurrentQuiz == null ? "" : $"Question {CurrentIndex + 1} of {CurrentQuiz.Questions.Count}";

        private bool _isNextButtonVisible;
        public bool IsNextButtonVisible
        {
            get => _isNextButtonVisible;
            set => SetProperty(ref _isNextButtonVisible, value);
        }

        public QuizPlayViewModel()
        {
            _quizService = new FirebaseQuizService();
            _userService = new FirebaseUserService();

            SelectAnswerCommand = new Command<string>(OnAnswerSelected);
            NextQuestionCommand = new Command(OnNextQuestion);
        }
        public bool IsCorrectAnswer(string chosenAnswer)
        {
            if (CurrentQuestion == null)
                return false;

            int index = Array.IndexOf(CurrentQuestion.Answers, chosenAnswer);

            return index == CurrentQuestion.CorrectAnswerIndex;
        }
        public void LoadQuiz(Quiz quiz)
        {
            CurrentQuiz = quiz;

            CurrentQuizTitle = quiz.Title;
            Score = 0;
            CurrentIndex = 0;

            UpdateQuestionUI();
        }

        private void UpdateQuestionUI()
        {
            if (CurrentQuiz == null || CurrentQuiz.Questions == null)
                return;

            if (CurrentIndex < CurrentQuiz.Questions.Count)
            {
                CurrentQuestion = CurrentQuiz.Questions[CurrentIndex];
                OnPropertyChanged(nameof(QuestionProgress));

                IsNextButtonVisible = false;
            }
        }

        private void OnAnswerSelected(string selectedAnswer)
        {
            if (CurrentQuestion == null)
                return;

            // Convert selected answer string to index
            int selectedIndex = Array.IndexOf(CurrentQuestion.Answers, selectedAnswer);

            if (selectedIndex == CurrentQuestion.CorrectAnswerIndex)
            {
                Score += CurrentQuiz.Points;
            }

            IsNextButtonVisible = true;
        }

        private async void OnNextQuestion()
        {
            CurrentIndex++;

            if (CurrentIndex >= CurrentQuiz.Questions.Count)
            {
                await SaveUserProgressAsync();

                int totalQuestions = CurrentQuiz.Questions.Count;

                await Shell.Current.Navigation.PushAsync(new QuizResultPage(Score, totalQuestions));
                return;
            }

            UpdateQuestionUI();
        }

        private async Task SaveUserProgressAsync()
        {
            var userId = Preferences.Get("UserId", string.Empty);

            if (string.IsNullOrEmpty(userId))
                return;

            var user = await _userService.GetUserByIdAsync(userId);

            if (user != null)
            {
                user.TotalPoints += Score;
                user.RecalculateRank();

                await _userService.UpdateUserAsync(userId, user);
            }
        }
    }
}
