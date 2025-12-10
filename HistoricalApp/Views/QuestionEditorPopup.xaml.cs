using CommunityToolkit.Maui.Views;
using HistoricalApp.Models;
using System;

namespace HistoricalApp.Views
{
    public partial class QuestionEditorPopup : Popup
    {
        public event Action<Question?> OnPopupClosed;
        private readonly Question _question;

        public QuestionEditorPopup(Question question)
        {
            InitializeComponent();
            _question = question ?? new Question();
            if (_question.Answers == null || _question.Answers.Length < 4)
                _question.Answers = new string[4];

            BindingContext = _question;
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            // Just invoke callback and let popup dismiss naturally
            try
            {
                Console.WriteLine("[DEBUG] Question saved, invoking callback");
                OnPopupClosed?.Invoke(_question);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Save Error] {ex.Message}");
            }
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            // Just invoke callback with null
            try
            {
                Console.WriteLine("[DEBUG] Question canceled, invoking callback");
                OnPopupClosed?.Invoke(null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Cancel Error] {ex.Message}");
            }
        }
    }
}
