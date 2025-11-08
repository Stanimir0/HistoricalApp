using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using HistoricalApp.Models;
using System;

namespace HistoricalApp.Views
{
    public partial class QuizEditorPopup : Popup
    {
        public event Action<Quiz?> OnPopupClosed;

        private readonly Quiz _quiz;

        public QuizEditorPopup(Quiz quiz = null)
        {
            InitializeComponent();

            _quiz = quiz ?? new Quiz();
            if (_quiz.Questions == null)
                _quiz.Questions = new List<Question>();

            BindingContext = _quiz;
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            Console.WriteLine($"[DEBUG] Saving quiz: {_quiz.Title}");
            ClosePopup(_quiz);
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            Console.WriteLine("[DEBUG] Popup canceled.");
            ClosePopup(null);
        }

        private async void OnManageQuestionsClicked(object sender, EventArgs e)
        {
            var popup = new QuestionManagerPopup(_quiz);
            await App.Current.MainPage.ShowPopupAsync(popup);
        }

        // ✅ Custom Close Method (replacement for Close/DismissAsync)
        private void ClosePopup(Quiz? result)
        {
            try
            {
                OnPopupClosed?.Invoke(result);
                this.Handler?.DisconnectHandler(); // Forcefully closes popup safely
                this.IsVisible = false;
                Console.WriteLine("[DEBUG] Popup closed manually.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Popup Close Error] {ex.Message}");
            }
        }
    }
}
