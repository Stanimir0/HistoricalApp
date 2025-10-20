using HistoricalApp.ViewModels;

namespace HistoricalApp.Views;

public partial class QuizSelectionPage : ContentPage
{
    public QuizSelectionPage()
    {
        InitializeComponent();
        BindingContext = new QuizSelectionViewModel();
    }
}