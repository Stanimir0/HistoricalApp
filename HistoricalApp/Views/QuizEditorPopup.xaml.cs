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
        private bool _isClosing = false;

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

        // ✅ Close popup with delay to prevent ArgumentException
        private void ClosePopup(Quiz? result)
        {
            if (_isClosing) return;
            _isClosing = true;

            try
            {
                Console.WriteLine($"[DEBUG] Closing popup with result: {result?.Title ?? "null"}");
                
                // Invoke callback
                OnPopupClosed?.Invoke(result);
                
                // Schedule close on main thread after a tiny delay
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(50);
                    try
                    {
                        await CloseAsync();
                    }
                    catch (Exception closeEx)
                    {
                        Console.WriteLine($"[Close Error] {closeEx.Message}");
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Popup Error] {ex.Message}");
                _isClosing = false;
            }
        }
    }
}
