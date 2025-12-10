using CommunityToolkit.Maui.Views;
using HistoricalApp.Models;
using HistoricalApp.ViewModels;
using System;

namespace HistoricalApp.Views
{
    public partial class QuestionManagerPopup : Popup
    {
        public event Action OnPopupClosed;
        private readonly Quiz _quiz;
        private bool _isClosing = false;

        public QuestionManagerPopup(Quiz quiz)
        {
            InitializeComponent();
            _quiz = quiz;
            BindingContext = new QuestionManagerViewModel(_quiz);
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        // ✅ Close popup with delay to prevent ArgumentException
        private void ClosePopup()
        {
            if (_isClosing) return;
            _isClosing = true;

            try
            {
                Console.WriteLine("[DEBUG] Closing QuestionManagerPopup");
                
                // Invoke callback
                OnPopupClosed?.Invoke();
                
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
