using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalApp.Helpers
{
    
    public static class RankHelper
    {
        public static Rank Calculate(int points)
        {
            if (points <= 100) return Rank.Bronze;
            if (points <= 200) return Rank.Silver;
            if (points <= 400) return Rank.Gold;
            if (points <= 700) return Rank.Diamond;
            return Rank.Historian;
        }
    }

}
