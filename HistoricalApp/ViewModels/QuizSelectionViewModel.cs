using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class QuizSelectionViewModel : BaseViewModel
    {
        public ICommand SelectBattleQuizCommand { get; }
        public ICommand SelectEventQuizCommand { get; }
        public ICommand SelectCharacterQuizCommand { get; }

        public QuizSelectionViewModel()
        {
            SelectBattleQuizCommand = new Command(OnBattleSelected);
            SelectEventQuizCommand = new Command(OnEventSelected);
            SelectCharacterQuizCommand = new Command(OnCharacterSelected);
        }

        private async void OnBattleSelected()
        {
            await App.Current.MainPage.DisplayAlert("Quiz Selected", "You chose Battles!", "OK");
            await App.Current.MainPage.Navigation.PushAsync(new Views.NewPage1());
        }

        private async void OnEventSelected()
        {
            await App.Current.MainPage.DisplayAlert("Quiz Selected", "You chose Events!", "OK");
            await App.Current.MainPage.Navigation.PushAsync(new Views.Events());
        }

        private async void OnCharacterSelected()
        {
            await App.Current.MainPage.DisplayAlert("Quiz Selected", "You chose Characters!", "OK");
            await App.Current.MainPage.Navigation.PushAsync(new Views.Characters());
        }
    }
}
