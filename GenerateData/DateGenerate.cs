using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class DateGenerate
    {
        private static Random rand = new Random((int)DateTime.Now.Ticks);

        public static DateTime GetRandom()
        {
            var year = rand.Next(300) + 1754;
            if (year > DateTime.Now.Year)
            {
                var diff = year - DateTime.Now.Year;
                year = year - diff;
            }
            
            return new DateTime(year, 1, 1).AddDays(rand.Next(365));
        }
    }
}
