using Firebase.Database;
using Firebase.Database.Query;
using HistoricalApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HistoricalApp.Services
{
    public class FirebaseQuizService
    {
        private readonly FirebaseClient _client;

        public FirebaseQuizService()
        {
            _client = new FirebaseClient("https://historical-f19c6-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task AddQuizAsync(Quiz quiz)
        {
            if (string.IsNullOrEmpty(quiz.Id))
                quiz.Id = Guid.NewGuid().ToString();

            if (quiz.Questions == null)
                quiz.Questions = new List<Question>();

            await _client.Child("quizzes")
                         .Child(quiz.Id)
                         .PutAsync(quiz);
        }

        public async Task<List<Quiz>> GetAllQuizzesAsync()
        {
            var data = await _client.Child("quizzes").OnceAsync<Quiz>();
            var quizzes = new List<Quiz>();
            foreach (var item in data)
            {
                var quiz = item.Object;
                quiz.Id = item.Key;

                if (quiz.Questions == null)
                    quiz.Questions = new List<Question>();

                // Sanitize Questions — fix null/incomplete answers and out-of-range CorrectAnswerIndex
                foreach (var q in quiz.Questions)
                {
                    if (q.Answers == null || q.Answers.Length < 4)
                    {
                        var newAnswers = new string[4];
                        if (q.Answers != null)
                        {
                            for (int i = 0; i < Math.Min(q.Answers.Length, 4); i++)
                                newAnswers[i] = q.Answers[i];
                        }
                        q.Answers = newAnswers;
                    }

                    // Trim whitespace and replace null entries
                    for (int i = 0; i < q.Answers.Length; i++)
                    {
                        if (q.Answers[i] == null)
                        {
                            q.Answers[i] = string.Empty;
                            System.Diagnostics.Debug.WriteLine($"[QuizService] Warning: null answer at index {i} in question '{q.Text}'");
                        }
                        else
                        {
                            q.Answers[i] = q.Answers[i].Trim();
                        }
                    }

                    // Validate CorrectAnswerIndex
                    if (q.CorrectAnswerIndex < 0 || q.CorrectAnswerIndex >= 4)
                    {
                        System.Diagnostics.Debug.WriteLine($"[QuizService] Warning: CorrectAnswerIndex {q.CorrectAnswerIndex} out of range for question '{q.Text}', resetting to 0");
                        q.CorrectAnswerIndex = 0;
                    }
                }

                quizzes.Add(quiz);
            }
            return quizzes;
        }

        public async Task UpdateQuizAsync(Quiz quiz)
        {
            await _client.Child("quizzes").Child(quiz.Id).PutAsync(quiz);
        }

        public async Task DeleteQuizAsync(string quizId)
        {
            await _client.Child("quizzes").Child(quizId).DeleteAsync();
        }
    }
}
