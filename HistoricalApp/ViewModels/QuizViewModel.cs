using HistoricalApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HistoricalApp.ViewModels
{
    public class QuizViewModel : BaseViewModel
    {
        private ObservableCollection<Question> questions;
        private int currentQuestionIndex;
        private int score;
        private Question currentQuestion;
        private int selectedAnswerIndex = -1;

        public ObservableCollection<Question> Questions
        {
            get => questions;
            set => SetProperty(ref questions, value);
        }

        public Question CurrentQuestion
        {
            get => currentQuestion;
            set => SetProperty(ref currentQuestion, value);
        }

        public int Score
        {
            get => score;
            set => SetProperty(ref score, value);
        }

        private string selectedAnswer;

        public string SelectedAnswer
        {
            get => selectedAnswer;
            set => SetProperty(ref selectedAnswer, value);
        }

        public ICommand SubmitAnswerCommand { get; }
        public ICommand NextQuestionCommand { get; }

        public QuizViewModel()
        {
           
            Questions = new ObservableCollection<Question>
        {
            new Question
            {
                Text = "When did the Battle of Hastings occur?",
                Choices = new List<string> { "1066", "1215", "1415", "1812" },
                CorrectAnswerIndex = 0
            },
            
        };

            currentQuestionIndex = 0;
            CurrentQuestion = Questions[currentQuestionIndex];
            Score = 0;

            SubmitAnswerCommand = new Command(SubmitAnswer);
            NextQuestionCommand = new Command(NextQuestion);
        }

        private void SubmitAnswer()
        {
            string correctAnswer = CurrentQuestion.Choices[CurrentQuestion.CorrectAnswerIndex];
            if (SelectedAnswer == CurrentQuestion.Choices[CurrentQuestion.CorrectAnswerIndex])
            {
                Score += 10; 
            }
        }

        private void NextQuestion()
        {
            if (currentQuestionIndex < Questions.Count - 1)
            {
                currentQuestionIndex++;
                CurrentQuestion = Questions[currentQuestionIndex];
                SelectedAnswer = null; 
            }
            else
            {
                
            }
        }
    }
}
