using HistoricalApp.Models;
using System;

namespace HistoricalApp.Views
{
    public partial class QuizEditorPopup
    {
      
        public event Action<Quiz?> OnPopupClosed;

        private readonly Quiz _quiz;

        public QuizEditorPopup(Quiz quiz = null)
        {
            InitializeComponent();
            _quiz = quiz ?? new Quiz();
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

      
        private void ClosePopup(Quiz? quiz)
        {
            OnPopupClosed?.Invoke(quiz); 
            this.IsVisible = false; 
        }
    }
}
