using HistoricalApp.Models;
using HistoricalApp.Services;
using HistoricalApp.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class QuizSelectionViewModel : BaseViewModel
    {
        private readonly FirebaseQuizService _quizService;
        public ObservableCollection<Quiz> Quizzes { get; set; } = new();
        public ICommand SelectQuizCommand { get; }

        private readonly string _category;
        public string CategoryTitle { get; }

        private bool _hasNoQuizzes;
        public bool HasNoQuizzes
        {
            get => _hasNoQuizzes;
            set => SetProperty(ref _hasNoQuizzes, value);
        }

        private bool _hasQuizzes;
        public bool HasQuizzes
        {
            get => _hasQuizzes;
            set => SetProperty(ref _hasQuizzes, value);
        }

        public QuizSelectionViewModel(string category)
        {
            _quizService = new FirebaseQuizService();
            _category = category;
            CategoryTitle = string.IsNullOrEmpty(category) ? "All Quizzes" : $"{category} Quizzes";

            SelectQuizCommand = new Command<Quiz>(async (quiz) => await GoToInfoPage(quiz));
            _ = LoadQuizzes();
        }

        private async Task LoadQuizzes()
        {
            var allQuizzes = await _quizService.GetAllQuizzesAsync();
            var filtered = string.IsNullOrEmpty(_category)
                ? allQuizzes
                : allQuizzes.Where(q => q.Category?.Equals(_category, StringComparison.OrdinalIgnoreCase) == true);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Quizzes.Clear();
                foreach (var quiz in filtered)
                    Quizzes.Add(quiz);

                HasQuizzes = Quizzes.Any();
                HasNoQuizzes = !HasQuizzes;
            });
        }

        private async Task GoToInfoPage(Quiz quiz)
        {
            await App.Current.MainPage.Navigation.PushAsync(new QuizInfoPage(quiz));
        }
    }
}
