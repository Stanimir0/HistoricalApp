using CommunityToolkit.Maui.Extensions;
using Firebase.Database;
using Firebase.Database.Query;
using HistoricalApp.Models;
using HistoricalApp.Services;
using HistoricalApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        private readonly FirebaseQuizService _quizService;

        public ObservableCollection<Quiz> Quizzes { get; set; } = new();

        public ICommand AddQuizCommand { get; }
        public ICommand EditQuizCommand { get; }
        public ICommand DeleteQuizCommand { get; }
        public ICommand TestConnectionCommand { get; }

        public AdminViewModel()
        {
            _quizService = new FirebaseQuizService();

            AddQuizCommand = new Command(async () => await AddQuiz());
            EditQuizCommand = new Command<Quiz>(async (quiz) => await EditQuiz(quiz));
            DeleteQuizCommand = new Command<Quiz>(async (quiz) => await DeleteQuiz(quiz));
            TestConnectionCommand = new Command(async () => await TestFirebaseConnection());

            _ = LoadQuizzes();
        }

        private async Task LoadQuizzes()
        {
            var quizzes = await _quizService.GetAllQuizzesAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Quizzes.Clear();
                foreach (var q in quizzes)
                    Quizzes.Add(q);
            });
        }

        private async Task AddQuiz()
        {
            var popup = new QuizEditorPopup();

            popup.OnPopupClosed += async (newQuiz) =>
            {
                if (newQuiz == null) return;

                if (newQuiz.Questions == null)
                    newQuiz.Questions = new List<Question>();

                await _quizService.AddQuizAsync(newQuiz);
                await App.Current.MainPage.DisplayAlert("Success", "Quiz added!", "OK");

                await LoadQuizzes();
            };

            await App.Current.MainPage.ShowPopupAsync(popup);
        }

        private async Task EditQuiz(Quiz quiz)
        {
            var popup = new QuizEditorPopup(quiz);

            popup.OnPopupClosed += async (updatedQuiz) =>
            {
                if (updatedQuiz == null) return;

                updatedQuiz.Id = quiz.Id;

                if (updatedQuiz.Questions == null)
                    updatedQuiz.Questions = quiz.Questions;

                await _quizService.UpdateQuizAsync(updatedQuiz);
                await App.Current.MainPage.DisplayAlert("Success", "Quiz updated!", "OK");

                await LoadQuizzes();
            };

            await App.Current.MainPage.ShowPopupAsync(popup);
        }

        private async Task DeleteQuiz(Quiz quiz)
        {
            bool confirm = await App.Current.MainPage.DisplayAlert(
                "Confirm Delete",
                $"Delete quiz: {quiz.Title}?",
                "Yes", "No");

            if (!confirm) return;

            await _quizService.DeleteQuizAsync(quiz.Id);
            await LoadQuizzes();
        }

        private async Task TestFirebaseConnection()
        {
            try
            {
                var client = new FirebaseClient("https://historical-f19c6-default-rtdb.europe-west1.firebasedatabase.app/");
                await client.Child("connectionTests").PostAsync(new
                {
                    message = "Connection successful!",
                    timestamp = DateTime.UtcNow.ToString("O")
                });

                await App.Current.MainPage.DisplayAlert("Firebase", "Connection OK!", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
