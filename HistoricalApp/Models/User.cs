using HistoricalApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalApp.Models
{
    internal class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int TotalPoints { get; set; }
        public Rank Rank => RankHelper.Calculate(TotalPoints);
    }
}
