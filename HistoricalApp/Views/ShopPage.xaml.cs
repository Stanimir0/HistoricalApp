using HistoricalApp.ViewModels;

namespace HistoricalApp.Views
{
    public partial class ShopPage : ContentPage
    {
        private readonly ShopViewModel _viewModel;

        public ShopPage()
        {
            InitializeComponent();
            _viewModel = (ShopViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel.LoadShopCommand.CanExecute(null))
            {
                _viewModel.LoadShopCommand.Execute(null);
            }
        }
    }
}
