using HistoricalApp.Models;
using HistoricalApp.Services;
using HistoricalApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class QuizSelectionViewModel : BaseViewModel
    {
        private readonly FirebaseQuizService _quizService;

        public ObservableCollection<Quiz> Quizzes { get; set; } = new();

        public string CategoryTitle
        {
            get => _categoryTitle;
            set => SetProperty(ref _categoryTitle, value);
        }
        private string _categoryTitle;

        public ICommand SelectQuizCommand { get; }

        public QuizSelectionViewModel(string category)
        {
            _quizService = new FirebaseQuizService();
            SelectQuizCommand = new Command<Quiz>(async (quiz) => await OnQuizSelected(quiz));

            LoadQuizzes(category).ConfigureAwait(false);
        }

        private async Task LoadQuizzes(string category)
        {
            CategoryTitle = $"{category} Quizzes";

            var quizzes = await _quizService.GetAllQuizzesAsync();

            var filtered = quizzes
                .Where(q => q.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Quizzes.Clear();
            foreach (var quiz in filtered)
                Quizzes.Add(quiz);
        }

        public async void UpdateCategory(string category)
        {
            await LoadQuizzes(category);
        }

        private async Task OnQuizSelected(Quiz quiz)
        {
            if (quiz == null)
                return;

            await Shell.Current.Navigation.PushAsync(new QuizInfoPage(quiz));
        }
    }
}
