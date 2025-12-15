using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using HistoricalApp.Models;
using System;
using System.Collections.ObjectModel;

namespace HistoricalApp.Views
{
    public partial class QuizEditorPopup : Popup
    {
        public event Action<Quiz?> OnPopupClosed;

        private readonly Quiz _quiz;
        private Question? _editingOriginalQuestion; // Track if we are editing an existing question or adding new
        
        // Properties bound to UI
        public bool IsMainVisible { get; set; } = true;
        public bool IsOverlayVisible { get; set; } = false;
        public Question CurrentQuestion { get; set; } // The scratchpad question for the overlay

        public QuizEditorPopup(Quiz quiz = null)
        {
            InitializeComponent();

            _quiz = quiz ?? new Quiz();
            if (_quiz.Questions == null)
                _quiz.Questions = new List<Question>();

            // Set BindingContext to this popup so it can bind to IsMainVisible, IsOverlayVisible, CurrentQuestion AND _quiz properties
            // But wait, the XML binds to Title/Description which are on _quiz.
            // Let's make a wrapper or just set BindingContext = this and expose Quiz property?
            // Simpler: Set BindingContext = this. Expose Quiz properties or just bind to Quiz.Title?
            // The XML uses {Binding Title}. If I set BindingContext = this, I need checking.
            // Let's use a dynamic approach or composed ViewModel for this popup.
            // Quickest clean way: Merge properties.
            
            BindingContext = new QuizEditorViewModel(_quiz);
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            if (BindingContext is QuizEditorViewModel vm)
            {
                Console.WriteLine($"[DEBUG] Saving quiz: {vm.Title}");
                ClosePopup(vm.GetQuiz());
            }
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            Console.WriteLine("[DEBUG] Popup canceled.");
            ClosePopup(null);
        }

        private void OnAddQuestionClicked(object sender, EventArgs e)
        {
            if (BindingContext is QuizEditorViewModel vm)
            {
                vm.StartAddingQuestion();
            }
        }

        private void OnEditQuestionClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is Question question && BindingContext is QuizEditorViewModel vm)
            {
                vm.StartEditingQuestion(question);
            }
        }

        private void OnDeleteQuestionClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is Question question && BindingContext is QuizEditorViewModel vm)
            {
                vm.DeleteQuestion(question);
            }
        }

        private void OnOverlaySaveClicked(object sender, EventArgs e)
        {
            if (BindingContext is QuizEditorViewModel vm)
            {
                vm.SaveOverlay();
            }
        }

        private void OnOverlayCancelClicked(object sender, EventArgs e)
        {
            if (BindingContext is QuizEditorViewModel vm)
            {
                vm.CancelOverlay();
            }
        }

        // ✅ Close popup with delay
        private bool _isClosing = false;
        private void ClosePopup(Quiz? result)
        {
            if (_isClosing) return;
            _isClosing = true;

            try
            {
                OnPopupClosed?.Invoke(result);
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(50);
                    try { await CloseAsync(); } catch { }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Popup Error] {ex.Message}");
                _isClosing = false;
            }
        }
    }

    // Helper ViewModel for the Popup state
    public class QuizEditorViewModel : BindableObject
    {
        private readonly Quiz _originalQuiz;
        
        public QuizEditorViewModel(Quiz quiz)
        {
            _originalQuiz = quiz;
            Questions = new ObservableCollection<Question>(quiz.Questions);
        }

        // Quiz Properties
        public string Title { get => _originalQuiz.Title; set { _originalQuiz.Title = value; OnPropertyChanged(); } }
        public string Description { get => _originalQuiz.Description; set { _originalQuiz.Description = value; OnPropertyChanged(); } }
        public string Category { get => _originalQuiz.Category; set { _originalQuiz.Category = value; OnPropertyChanged(); } }
        public string Difficulty { get => _originalQuiz.Difficulty; set { _originalQuiz.Difficulty = value; OnPropertyChanged(); } }
        public int Points { get => _originalQuiz.Points; set { _originalQuiz.Points = value; OnPropertyChanged(); } }

        public ObservableCollection<Question> Questions { get; set; }

        // Overlay State
        private bool _isMainVisible = true;
        public bool IsMainVisible { get => _isMainVisible; set { _isMainVisible = value; OnPropertyChanged(); } }

        private bool _isOverlayVisible = false;
        public bool IsOverlayVisible { get => _isOverlayVisible; set { _isOverlayVisible = value; OnPropertyChanged(); } }

        private Question _currentQuestion;
        public Question CurrentQuestion { get => _currentQuestion; set { _currentQuestion = value; OnPropertyChanged(); } }

        private Question _originalBeingEdited;

        public void StartAddingQuestion()
        {
            var newQ = new Question { Answers = new string[4] };
            CurrentQuestion = newQ;
            _originalBeingEdited = null;
            ShowOverlay();
        }

        public void StartEditingQuestion(Question q)
        {
            // Deep copy to prevent modifying until saved
            CurrentQuestion = new Question
            {
                Id = q.Id,
                Text = q.Text,
                CorrectAnswerIndex = q.CorrectAnswerIndex,
                Answers = (string[])q.Answers.Clone()
            };
            _originalBeingEdited = q;
            ShowOverlay();
        }

        public void DeleteQuestion(Question q)
        {
            Questions.Remove(q);
            _originalQuiz.Questions.Remove(q);
        }

        public void SaveOverlay()
        {
            if (_originalBeingEdited == null)
            {
                // Add Mode
                Questions.Add(CurrentQuestion);
                _originalQuiz.Questions.Add(CurrentQuestion);
            }
            else
            {
                // Edit Mode
                var index = Questions.IndexOf(_originalBeingEdited);
                if (index != -1)
                {
                    Questions[index] = CurrentQuestion;
                    _originalQuiz.Questions[index] = CurrentQuestion;
                }
            }
            HideOverlay();
        }

        public void CancelOverlay()
        {
            HideOverlay();
        }

        private void ShowOverlay()
        {
            IsMainVisible = false;
            IsOverlayVisible = true;
        }

        private void HideOverlay()
        {
            IsMainVisible = true;
            IsOverlayVisible = false;
            CurrentQuestion = null;
        }

        public Quiz GetQuiz() => _originalQuiz;
    }
}
