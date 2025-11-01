using Firebase.Database;
using Firebase.Database.Query;
using HistoricalApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoricalApp.Services
{
    public class FirebaseQuizService
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseQuizService()
        {
            _firebaseClient = new FirebaseClient("https://historical-f19c6-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task AddQuizAsync(Quiz quiz)
        {
            await _firebaseClient
                .Child("quizzes")
                .PostAsync(quiz);
        }

        public async Task<List<Quiz>> GetAllQuizzesAsync()
        {
            var quizzes = await _firebaseClient
                .Child("quizzes")
                .OnceAsync<Quiz>();

            return quizzes.Select(item =>
            {
                var q = item.Object;
                q.Id = item.Key;
                return q;
            }).ToList();
        }

        public async Task UpdateQuizAsync(Quiz quiz)
        {
            await _firebaseClient
                .Child("quizzes")
                .Child(quiz.Id)
                .PutAsync(quiz);
        }

        public async Task DeleteQuizAsync(string id)
        {
            await _firebaseClient
                .Child("quizzes")
                .Child(id)
                .DeleteAsync();
        }
    }
}
