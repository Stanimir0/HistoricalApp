namespace HistoricalApp.Views
{
    public partial class QuizResultPage : ContentPage
    {
        public int Score { get; }
        public int TotalQuestions { get; }

        public string ScoreText => $"You scored {Score} points.";
        public string PointsInfo => $"Total Questions: {TotalQuestions}";

        public QuizResultPage(int score, int totalQuestions)
        {
            InitializeComponent();

            Score = score;
            TotalQuestions = totalQuestions;

            BindingContext = this;
        }

        private async void OnBackHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

        private async void OnPlayAnotherClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//CategorySelectionPage");
        }
    }
}
