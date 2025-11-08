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
                quiz.Questions = new List<Question>(); // ✅ Prevent null reference

            await _client.Child("quizzes")
                         .Child(quiz.Id)
                         .PutAsync(quiz); // ✅ Stores entire quiz object (with questions)
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
                    quiz.Questions = new List<Question>(); // ✅ Always safe

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
