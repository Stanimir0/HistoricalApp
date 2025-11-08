using System.Collections.Generic;

namespace HistoricalApp.Models
{
    public class Quiz
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Difficulty { get; set; }
        public int Points { get; set; }

        // 🆕 Add questions
        public List<Question> Questions { get; set; } = new();
    }

    public class Question
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Text { get; set; }
        public string[] Answers { get; set; } = new string[4];
        public int CorrectAnswerIndex { get; set; }
    }
}
