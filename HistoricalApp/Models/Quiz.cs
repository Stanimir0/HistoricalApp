using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalApp.Models
{
    public class Quiz
    {
        public string Id { get; set; }              
        public string Title { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public int Points { get; set; }
        public string Category { get; set; }
        public Dictionary<string, Question> Questions { get; set; }

        public Quiz()
        {
            Questions = new Dictionary<string, Question>();
        }
    }
}
