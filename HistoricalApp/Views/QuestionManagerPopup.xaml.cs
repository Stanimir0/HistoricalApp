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

        // ✅ Custom Close
        private void ClosePopup()
        {
            try
            {
                OnPopupClosed?.Invoke();
                this.Handler?.DisconnectHandler();
                this.IsVisible = false;
                Console.WriteLine("[DEBUG] QuestionManagerPopup closed manually.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Popup Close Error] {ex.Message}");
            }
        }
    }
}
