using HistoricalApp.Helpers;
using Microsoft.Maui.Controls;

namespace HistoricalApp.Views
{
    [QueryProperty(nameof(Category), "category")]
    public partial class QuizSelectionPage : ContentPage
    {
        private string _category = "All";

        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                // Update existing ViewModel with new category
                if (BindingContext is ViewModels.QuizSelectionViewModel vm)
                {
                    vm.UpdateCategory(value);
                }
            }
        }

        public QuizSelectionPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.QuizSelectionViewModel("All");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AnimationHelper.SlideUpFadeIn(RootLayout);
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }
    }
}
