using HistoricalApp.ViewModels;

namespace HistoricalApp.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ProfileViewModel vm)
                vm.LoadUserCommand.Execute(null);
        }
    }
}
