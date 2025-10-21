using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using HistoricalApp.Models;
using HistoricalApp.Services;
using HistoricalApp.Views;
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

        public AdminViewModel()
        {
            _quizService = new FirebaseQuizService();

            AddQuizCommand = new Command(async () => await AddQuiz());
            EditQuizCommand = new Command<Quiz>(async (quiz) => await EditQuiz(quiz));
            DeleteQuizCommand = new Command<Quiz>(async (quiz) => await DeleteQuiz(quiz));

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
            var newQuiz = new Quiz(); 
            var popup = new QuizEditorPopup(newQuiz); 
            var result = await App.Current.MainPage.ShowPopupAsync(popup) as Quiz;

            if (result != null)
            {
                await _quizService.AddQuizAsync(result);
                await LoadQuizzes();
            }
        }

        private async Task EditQuiz(Quiz quiz)
        {
            var popup = new QuizEditorPopup(quiz);
            var result = await App.Current.MainPage.ShowPopupAsync(popup) as Quiz;

            if (result != null)
            {
                result.Id = quiz.Id;
                await _quizService.UpdateQuizAsync(result);
                await LoadQuizzes();
            }
        }

        private async Task DeleteQuiz(Quiz quiz)
        {
            await _quizService.DeleteQuizAsync(quiz.Id);
            await LoadQuizzes();
        }
    }
}
