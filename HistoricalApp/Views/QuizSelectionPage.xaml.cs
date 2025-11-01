using HistoricalApp.ViewModels;

namespace HistoricalApp.Views
{
    public partial class QuizSelectionPage : ContentPage
    {
        public QuizSelectionPage(string category)
        {
            InitializeComponent();
            BindingContext = new QuizSelectionViewModel(category);
        }

        public QuizSelectionPage() : this(string.Empty)
        {
        }
    }
}
