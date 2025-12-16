using HistoricalApp.Models;
using HistoricalApp.ViewModels;

namespace HistoricalApp.Views
{
    public partial class EditProfilePage : ContentPage
    {
        public EditProfilePage(User user)
        {
            InitializeComponent();
            BindingContext = new EditProfileViewModel(user);
        }
    }
}
