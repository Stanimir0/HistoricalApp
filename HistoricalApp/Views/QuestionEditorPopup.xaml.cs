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
            ClosePopup(_question);
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            ClosePopup(null);
        }

        // ✅ Custom close for your .NET version
        private void ClosePopup(Question? result)
        {
            try
            {
                OnPopupClosed?.Invoke(result);
                this.Handler?.DisconnectHandler();
                this.IsVisible = false;
                Console.WriteLine("[DEBUG] QuestionEditorPopup closed manually.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Popup Close Error] {ex.Message}");
            }
        }
    }
}
