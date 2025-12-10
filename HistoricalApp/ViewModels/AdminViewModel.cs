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
            try
            {
                var quizzes = await _quizService.GetAllQuizzesAsync();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Quizzes.Clear();
                    foreach (var q in quizzes)
                        Quizzes.Add(q);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] LoadQuizzes failed: {ex.Message}");
                if (App.Current?.MainPage != null)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Failed to load quizzes. Please check your internet connection.", "OK");
                    });
                }
            }
        }

        private async Task AddQuiz()
        {
            try
            {
                var popup = new QuizEditorPopup();

                popup.OnPopupClosed += async (newQuiz) =>
                {
                    if (newQuiz == null) return;

                    try
                    {
                        if (newQuiz.Questions == null)
                            newQuiz.Questions = new List<Question>();

                        await _quizService.AddQuizAsync(newQuiz);
                        
                        if (App.Current?.MainPage != null)
                            await App.Current.MainPage.DisplayAlert("Success", "Quiz added successfully!", "OK");

                        await LoadQuizzes();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Error] AddQuiz failed: {ex.Message}");
                        if (App.Current?.MainPage != null)
                            await App.Current.MainPage.DisplayAlert("Error", $"Failed to add quiz: {ex.Message}", "OK");
                    }
                };

                if (App.Current?.MainPage != null)
                    await App.Current.MainPage.ShowPopupAsync(popup);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to show popup: {ex.Message}");
                if (App.Current?.MainPage != null)
                    await App.Current.MainPage.DisplayAlert("Error", "Failed to open quiz editor.", "OK");
            }
        }

        private async Task EditQuiz(Quiz quiz)
        {
            if (quiz == null)
            {
                Console.WriteLine("[Error] Cannot edit null quiz");
                return;
            }

            try
            {
                var popup = new QuizEditorPopup(quiz);

                popup.OnPopupClosed += async (updatedQuiz) =>
                {
                    if (updatedQuiz == null) return;

                    try
                    {
                        updatedQuiz.Id = quiz.Id;

                        if (updatedQuiz.Questions == null)
                            updatedQuiz.Questions = quiz.Questions ?? new List<Question>();

                        await _quizService.UpdateQuizAsync(updatedQuiz);
                        
                        if (App.Current?.MainPage != null)
                            await App.Current.MainPage.DisplayAlert("Success", "Quiz updated successfully!", "OK");

                        await LoadQuizzes();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Error] EditQuiz failed: {ex.Message}");
                        if (App.Current?.MainPage != null)
                            await App.Current.MainPage.DisplayAlert("Error", $"Failed to update quiz: {ex.Message}", "OK");
                    }
                };

                if (App.Current?.MainPage != null)
                    await App.Current.MainPage.ShowPopupAsync(popup);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to show edit popup: {ex.Message}");
                if (App.Current?.MainPage != null)
                    await App.Current.MainPage.DisplayAlert("Error", "Failed to open quiz editor.", "OK");
            }
        }

        private async Task DeleteQuiz(Quiz quiz)
        {
            if (quiz == null)
            {
                Console.WriteLine("[Error] Cannot delete null quiz");
                return;
            }

            try
            {
                if (App.Current?.MainPage == null)
                {
                    Console.WriteLine("[Error] MainPage is null, cannot show alert");
                    return;
                }

                bool confirm = await App.Current.MainPage.DisplayAlert(
                    "Confirm Delete",
                    $"Delete quiz: {quiz.Title}?",
                    "Yes", "No");

                if (!confirm) return;

                await _quizService.DeleteQuizAsync(quiz.Id);
                await LoadQuizzes();
                
                await App.Current.MainPage.DisplayAlert("Success", "Quiz deleted successfully!", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] DeleteQuiz failed: {ex.Message}");
                if (App.Current?.MainPage != null)
                    await App.Current.MainPage.DisplayAlert("Error", $"Failed to delete quiz: {ex.Message}", "OK");
            }
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
