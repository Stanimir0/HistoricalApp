using HistoricalApp.Helpers;

namespace HistoricalApp.Views
{
    public partial class QuizSelectionPage : ContentPage
    {
        public QuizSelectionPage(string category)
        {
            InitializeComponent();
            BindingContext = new ViewModels.QuizSelectionViewModel(category);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AnimationHelper.SlideUpFadeIn(RootLayout);
        }
    }
}
