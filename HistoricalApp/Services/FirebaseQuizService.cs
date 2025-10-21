using Firebase.Database;
using Firebase.Database.Query;
using HistoricalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            await _firebaseClient.Child("quizzes").PostAsync(quiz);
        }

      
        public async Task<List<Quiz>> GetAllQuizzesAsync()
        {
            var quizzes = await _firebaseClient
                .Child("quizzes")
                .OnceAsync<Quiz>();

            return quizzes.Select(q =>
            {
                q.Object.Id = q.Key;
                return q.Object;
            }).ToList();
        }

       
        public async Task<Quiz> GetQuizByIdAsync(string quizId)
        {
            var quiz = await _firebaseClient
                .Child("quizzes")
                .Child(quizId)
                .OnceSingleAsync<Quiz>();

            quiz.Id = quizId;
            return quiz;
        }

       
        public async Task UpdateQuizAsync(Quiz quiz)
        {
            await _firebaseClient
                .Child("quizzes")
                .Child(quiz.Id)
                .PutAsync(quiz);
        }

        public async Task DeleteQuizAsync(string quizId)
        {
            await _firebaseClient
                .Child("quizzes")
                .Child(quizId)
                .DeleteAsync();
        }
    }
}
