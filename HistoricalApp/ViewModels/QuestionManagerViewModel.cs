using CommunityToolkit.Maui.Extensions;
using HistoricalApp.Models;
using HistoricalApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class QuestionManagerViewModel : BaseViewModel
    {
        private readonly Quiz _quiz;

        public ObservableCollection<Question> Questions { get; set; }
        public ICommand AddQuestionCommand { get; }
        public ICommand EditQuestionCommand { get; }
        public ICommand DeleteQuestionCommand { get; }

        public QuestionManagerViewModel(Quiz quiz)
        {
            _quiz = quiz;
            Questions = new ObservableCollection<Question>(_quiz.Questions ?? new List<Question>());

            AddQuestionCommand = new Command(async () => await AddQuestion());
            EditQuestionCommand = new Command<Question>(async (q) => await EditQuestion(q));
            DeleteQuestionCommand = new Command<Question>(async (q) => await DeleteQuestion(q));
        }

        private async Task AddQuestion()
        {
            var popup = new QuestionEditorPopup(new Question());
            popup.OnPopupClosed += (result) =>
            {
                if (result != null)
                {
                    Questions.Add(result);
                    _quiz.Questions.Add(result);
                }
            };
            await App.Current.MainPage.ShowPopupAsync(popup);
        }

        private async Task EditQuestion(Question question)
        {
            var popup = new QuestionEditorPopup(question);
            popup.OnPopupClosed += (result) =>
            {
                if (result != null)
                {
                    var index = Questions.IndexOf(question);
                    Questions[index] = result;
                    _quiz.Questions[index] = result;
                }
            };
            await App.Current.MainPage.ShowPopupAsync(popup);
        }

        private async Task DeleteQuestion(Question question)
        {
            bool confirm = await App.Current.MainPage.DisplayAlert(
                "Confirm Delete",
                $"Are you sure you want to delete this question?",
                "Yes", "No");

            if (confirm)
            {
                Questions.Remove(question);
                _quiz.Questions.Remove(question);
            }
        }
    }
}
