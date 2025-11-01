using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using Firebase.Database;
using Firebase.Database.Query;
using HistoricalApp.Models;
using HistoricalApp.Services;
using HistoricalApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        public ICommand TestConnectionCommand { get; } // ✅ New command

        public AdminViewModel()
        {
            _quizService = new FirebaseQuizService();

            AddQuizCommand = new Command(async () => await AddQuiz());
            EditQuizCommand = new Command<Quiz>(async (quiz) => await EditQuiz(quiz));
            DeleteQuizCommand = new Command<Quiz>(async (quiz) => await DeleteQuiz(quiz));
            TestConnectionCommand = new Command(async () => await TestFirebaseConnection()); // ✅

            _ = LoadQuizzes();
        }

        private async Task LoadQuizzes()
        {
            var quizzes = await _quizService.GetAllQuizzesAsync();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Quizzes.Clear();
                foreach (var quiz in quizzes)
                    Quizzes.Add(quiz);
            });
        }

        private async Task AddQuiz()
        {
            var popup = new QuizEditorPopup();
            popup.OnPopupClosed += async (quiz) =>
            {
                if (quiz == null)
                {
                    Console.WriteLine("[DEBUG] Popup returned null.");
                    return;
                }

                Console.WriteLine($"[DEBUG] Popup returned quiz: {quiz.Title}");
                await _quizService.AddQuizAsync(quiz);
                await App.Current.MainPage.DisplayAlert("✅ Success", "Quiz saved to Firebase!", "OK");
                await LoadQuizzes();
            };

          
            await App.Current.MainPage.ShowPopupAsync(popup);
        }


        private async Task EditQuiz(Quiz quiz)
        {
            if (quiz == null)
                return;

        
            var popup = new QuizEditorPopup(new Quiz
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                Category = quiz.Category,
                Difficulty = quiz.Difficulty,
                Points = quiz.Points
            });

          
            popup.OnPopupClosed += async (updatedQuiz) =>
            {
                if (updatedQuiz == null)
                {
                    Console.WriteLine("[DEBUG] Edit canceled.");
                    return;
                }

             
                updatedQuiz.Id = quiz.Id;

                try
                {
                    await _quizService.UpdateQuizAsync(updatedQuiz);
                    await App.Current.MainPage.DisplayAlert("✅ Updated", "Quiz updated successfully!", "OK");
                    await LoadQuizzes();
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("❌ Error", ex.Message, "OK");
                    Console.WriteLine($"[ERROR] Update failed: {ex}");
                }
            };

            await App.Current.MainPage.ShowPopupAsync(popup);
        }

        private async Task DeleteQuiz(Quiz quiz)
        {
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

                await App.Current.MainPage.DisplayAlert("✅ Firebase", "Connection successful!", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("❌ Firebase Error", ex.Message, "OK");
            }
        }
    }
}
