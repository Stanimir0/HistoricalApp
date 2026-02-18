using HistoricalApp.Helpers;
using HistoricalApp.Models;
using HistoricalApp.Services;
using HistoricalApp.Views;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class QuizPlayViewModel : BaseViewModel
    {
        private readonly FirebaseQuizService _quizService;
        private readonly FirebaseUserService _userService;
        private IDispatcherTimer _timer;
        private bool _hasAnswered;

        public ICommand SelectAnswerCommand { get; }
        public ICommand UseFiftyFiftyCommand { get; }
        public ICommand UseDoublePointsCommand { get; }

        public Quiz CurrentQuiz { get; private set; }

        public TranslationService Translations => TranslationService.Instance;

        private Question _currentQuestion;
        public Question CurrentQuestion
        {
            get => _currentQuestion;
            set => SetProperty(ref _currentQuestion, value);
        }

        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (SetProperty(ref _currentIndex, value))
                    UpdateQuestionUI();
            }
        }

        private int _score;
        public int Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
        }

        public string CurrentQuizTitle { get; set; }

        public string QuestionProgress =>
            CurrentQuiz == null ? "" : $"Question {CurrentIndex + 1} of {CurrentQuiz.Questions.Count}";

        private double _questionProgressValue;
        public double QuestionProgressValue
        {
            get => _questionProgressValue;
            set => SetProperty(ref _questionProgressValue, value);
        }

        // === Timer ===
        private bool _isTimeBased;
        public bool IsTimeBased
        {
            get => _isTimeBased;
            set => SetProperty(ref _isTimeBased, value);
        }

        private int _remainingTime;
        public int RemainingTime
        {
            get => _remainingTime;
            set => SetProperty(ref _remainingTime, value);
        }

        private string _timerColor = "#4CAF50";
        public string TimerColor
        {
            get => _timerColor;
            set => SetProperty(ref _timerColor, value);
        }

        // === Hints ===
        private int _fiftyFiftyCount;
        public int FiftyFiftyCount
        {
            get => _fiftyFiftyCount;
            set
            {
                SetProperty(ref _fiftyFiftyCount, value);
                OnPropertyChanged(nameof(HasFiftyFifty));
            }
        }

        private int _doublePointsCount;
        public int DoublePointsCount
        {
            get => _doublePointsCount;
            set
            {
                SetProperty(ref _doublePointsCount, value);
                OnPropertyChanged(nameof(HasDoublePoints));
            }
        }

        public bool HasFiftyFifty => FiftyFiftyCount > 0;
        public bool HasDoublePoints => DoublePointsCount > 0 && !IsDoublePointsActive;

        private bool _isDoublePointsActive;
        public bool IsDoublePointsActive
        {
            get => _isDoublePointsActive;
            set
            {
                SetProperty(ref _isDoublePointsActive, value);
                OnPropertyChanged(nameof(HasDoublePoints));
            }
        }

        // Visible answers (may have items hidden by 50/50)
        private ObservableCollection<AnswerOption> _visibleAnswers = new();
        public ObservableCollection<AnswerOption> VisibleAnswers
        {
            get => _visibleAnswers;
            set => SetProperty(ref _visibleAnswers, value);
        }

        // Answer selected index for color feedback
        private int _selectedAnswerIndex = -1;
        public int SelectedAnswerIndex
        {
            get => _selectedAnswerIndex;
            set => SetProperty(ref _selectedAnswerIndex, value);
        }

        // === Result data passed to QuizResultPage ===
        public int LevelsGained { get; private set; }
        public int CoinsFromLevelUp { get; private set; }
        public int NewLevel { get; private set; }
        public int StreakCount { get; private set; }
        public int StreakBonusCoins { get; private set; }
        public int MissionCoins { get; private set; }
        public string SecretBadgeName { get; private set; }
        public string SecretBadgeEmoji { get; private set; }
        public int CorrectAnswers { get; private set; }

        private int _pointMultiplier = 1;

        public QuizPlayViewModel()
        {
            _quizService = new FirebaseQuizService();
            _userService = new FirebaseUserService();

            SelectAnswerCommand = new Command<string>(OnAnswerSelected);
            UseFiftyFiftyCommand = new Command(OnUseFiftyFifty);
            UseDoublePointsCommand = new Command(OnUseDoublePoints);
        }

        public bool IsCorrectAnswer(string chosenAnswer)
        {
            if (CurrentQuestion == null || string.IsNullOrEmpty(chosenAnswer))
                return false;

            // Trim for safety
            chosenAnswer = chosenAnswer.Trim();

            int index = -1;
            for (int i = 0; i < CurrentQuestion.Answers.Length; i++)
            {
                if (CurrentQuestion.Answers[i] != null &&
                    CurrentQuestion.Answers[i].Trim().Equals(chosenAnswer, StringComparison.Ordinal))
                {
                    index = i;
                    break;
                }
            }

            return index == CurrentQuestion.CorrectAnswerIndex;
        }

        public async Task LoadQuizAsync(Quiz quiz)
        {
            CurrentQuiz = quiz;
            CurrentQuizTitle = quiz.Title;
            Score = 0;
            CorrectAnswers = 0;
            _pointMultiplier = 1;
            IsTimeBased = quiz.IsTimeBased;
            IsDoublePointsActive = false;

            // Load user's hint inventory
            var userId = Preferences.Get("UserId", string.Empty);
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userService.GetUserByIdAsync(userId);
                if (user != null)
                {
                    FiftyFiftyCount = user.HintFiftyFifty;
                    DoublePointsCount = user.HintDoublePoints;
                }
            }

            CurrentIndex = 0;
            UpdateQuestionUI();
        }

        // Keep sync version for backward compat
        public void LoadQuiz(Quiz quiz)
        {
            CurrentQuiz = quiz;
            CurrentQuizTitle = quiz.Title;
            Score = 0;
            CorrectAnswers = 0;
            _pointMultiplier = 1;
            IsTimeBased = quiz.IsTimeBased;
            IsDoublePointsActive = false;
            CurrentIndex = 0;
            UpdateQuestionUI();
        }

        private void UpdateQuestionUI()
        {
            if (CurrentQuiz == null || CurrentQuiz.Questions == null)
                return;

            if (CurrentIndex < CurrentQuiz.Questions.Count)
            {
                CurrentQuestion = CurrentQuiz.Questions[CurrentIndex];
                OnPropertyChanged(nameof(QuestionProgress));

                // Update progress bar
                QuestionProgressValue = (double)(CurrentIndex) / CurrentQuiz.Questions.Count;

                _hasAnswered = false;
                SelectedAnswerIndex = -1;

                // Build answer options
                BuildVisibleAnswers();

                // Start timer if timed
                if (IsTimeBased)
                {
                    StartTimer();
                }
            }
        }

        private void BuildVisibleAnswers()
        {
            VisibleAnswers.Clear();
            if (CurrentQuestion == null) return;

            for (int i = 0; i < CurrentQuestion.Answers.Length; i++)
            {
                var answer = CurrentQuestion.Answers[i];
                if (!string.IsNullOrEmpty(answer))
                {
                    VisibleAnswers.Add(new AnswerOption
                    {
                        Text = answer,
                        Index = i,
                        IsVisible = true,
                        IsCorrect = i == CurrentQuestion.CorrectAnswerIndex,
                        State = AnswerState.Default
                    });
                }
            }
        }

        private void StartTimer()
        {
            StopTimer();
            RemainingTime = CurrentQuiz.TimeLimitSeconds;
            TimerColor = "#4CAF50"; // Green

            _timer = Application.Current.Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    RemainingTime--;

                    // Update color based on remaining time
                    double ratio = (double)RemainingTime / CurrentQuiz.TimeLimitSeconds;
                    if (ratio <= 0.25)
                        TimerColor = "#F44336"; // Red
                    else if (ratio <= 0.5)
                        TimerColor = "#FF9800"; // Orange

                    if (RemainingTime <= 0)
                    {
                        StopTimer();
                        if (!_hasAnswered)
                        {
                            // Time's up — auto-advance, no points
                            _hasAnswered = true;
                            AutoAdvanceAfterDelay();
                        }
                    }
                });
            };
            _timer.Start();
        }

        private void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer = null;
            }
        }

        private void OnAnswerSelected(string selectedAnswer)
        {
            if (CurrentQuestion == null || _hasAnswered)
                return;

            _hasAnswered = true;
            StopTimer();

            // Trim for comparison
            selectedAnswer = selectedAnswer?.Trim();

            // Find the selected answer option
            int selectedIndex = -1;
            for (int i = 0; i < CurrentQuestion.Answers.Length; i++)
            {
                if (CurrentQuestion.Answers[i] != null &&
                    CurrentQuestion.Answers[i].Trim().Equals(selectedAnswer, StringComparison.Ordinal))
                {
                    selectedIndex = i;
                    break;
                }
            }

            bool isCorrect = selectedIndex == CurrentQuestion.CorrectAnswerIndex;

            if (isCorrect)
            {
                int pointsEarned = CurrentQuiz.Points * _pointMultiplier;
                if (IsTimeBased) pointsEarned *= 2; // Time-based bonus
                Score += pointsEarned;
                CorrectAnswers++;
            }

            // Update answer states for visual feedback
            foreach (var option in VisibleAnswers)
            {
                if (option.Index == CurrentQuestion.CorrectAnswerIndex)
                    option.State = AnswerState.Correct;
                else if (option.Index == selectedIndex && !isCorrect)
                    option.State = AnswerState.Wrong;
                else
                    option.State = AnswerState.Dimmed;
            }

            SelectedAnswerIndex = selectedIndex;

            // Auto-advance after brief delay
            AutoAdvanceAfterDelay();
        }

        private async void AutoAdvanceAfterDelay()
        {
            // Brief pause so user can see correct/wrong feedback
            await Task.Delay(1200);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                CurrentIndex++;

                if (CurrentIndex >= CurrentQuiz.Questions.Count)
                {
                    FinishQuiz();
                    return;
                }

                UpdateQuestionUI();
            });
        }

        private async void FinishQuiz()
        {
            StopTimer();
            await SaveUserProgressAsync();

            int totalQuestions = CurrentQuiz.Questions.Count;

            await Shell.Current.Navigation.PushAsync(new QuizResultPage(
                Score,
                totalQuestions,
                CorrectAnswers,
                LevelsGained,
                CoinsFromLevelUp,
                NewLevel,
                StreakCount,
                StreakBonusCoins,
                MissionCoins,
                SecretBadgeName,
                SecretBadgeEmoji,
                IsDoublePointsActive
            ));
        }

        private async Task SaveUserProgressAsync()
        {
            var userId = Preferences.Get("UserId", string.Empty);

            if (string.IsNullOrEmpty(userId))
                return;

            var user = await _userService.GetUserByIdAsync(userId);

            if (user != null)
            {
                // Award points (leaderboard) and XP (permanent leveling)
                user.TotalPoints += Score;
                user.TotalXP += Score;
                user.DailyPoints += Score;
                user.WeeklyPoints += Score;
                user.MonthlyPoints += Score;
                user.RecalculateRank();

                // Level up check
                var (levelsGained, coinsAwarded, newLevel) = LevelService.CheckAndProcessLevelUp(user);
                LevelsGained = levelsGained;
                CoinsFromLevelUp = coinsAwarded;
                NewLevel = newLevel;

                // Streak update
                var (newStreak, streakBonus) = StreakService.UpdateStreak(user);
                StreakCount = newStreak;
                StreakBonusCoins = streakBonus;

                // Daily mission progress
                int missionCoins = DailyMissionService.CheckMissionProgress(
                    user, CurrentQuiz, Score, CurrentQuiz.Questions.Count);
                MissionCoins = missionCoins;

                // Secret badge roll
                var secretBadge = SecretBadgeService.TryAwardSecretBadge(user);
                if (secretBadge != null)
                {
                    SecretBadgeName = secretBadge.Name;
                    SecretBadgeEmoji = secretBadge.Emoji;
                }

                // Save hint usage (50/50 and double points were consumed during play)
                user.HintFiftyFifty = FiftyFiftyCount;
                user.HintDoublePoints = DoublePointsCount;

                await _userService.UpdateUserAsync(userId, user);
            }
        }

        // === Hint: 50/50 ===
        private void OnUseFiftyFifty()
        {
            if (_hasAnswered || FiftyFiftyCount <= 0 || CurrentQuestion == null)
                return;

            FiftyFiftyCount--;

            // Find wrong answers to hide
            var wrongOptions = VisibleAnswers
                .Where(a => !a.IsCorrect && a.IsVisible)
                .ToList();

            var random = new Random();
            int toRemove = Math.Min(2, wrongOptions.Count);
            var toHide = wrongOptions.OrderBy(_ => random.Next()).Take(toRemove).ToList();

            foreach (var option in toHide)
            {
                option.IsVisible = false;
                option.State = AnswerState.Hidden;
            }
        }

        // === Hint: Double Points ===
        private void OnUseDoublePoints()
        {
            if (IsDoublePointsActive || DoublePointsCount <= 0)
                return;

            DoublePointsCount--;
            IsDoublePointsActive = true;
            _pointMultiplier = 2;
        }
    }

    // Helper class for answer display
    public class AnswerOption : BaseViewModel
    {
        public string Text { get; set; } = string.Empty;
        public int Index { get; set; }
        public bool IsCorrect { get; set; }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        private AnswerState _state = AnswerState.Default;
        public AnswerState State
        {
            get => _state;
            set
            {
                SetProperty(ref _state, value);
                OnPropertyChanged(nameof(BackgroundColor));
                OnPropertyChanged(nameof(TextColor));
            }
        }

        public string BackgroundColor => State switch
        {
            AnswerState.Correct => "#1B5E20",
            AnswerState.Wrong => "#B71C1C",
            AnswerState.Dimmed => "#111111",
            AnswerState.Hidden => "Transparent",
            _ => "#1A1A1A"
        };

        public string TextColor => State switch
        {
            AnswerState.Correct => "#4CAF50",
            AnswerState.Wrong => "#EF5350",
            AnswerState.Dimmed => "#555555",
            AnswerState.Hidden => "Transparent",
            _ => "White"
        };
    }

    public enum AnswerState
    {
        Default,
        Correct,
        Wrong,
        Dimmed,
        Hidden
    }
}
