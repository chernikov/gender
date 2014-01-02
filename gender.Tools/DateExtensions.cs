using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Tools
{
    public static class DateExtensions
    {
            public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
            {
                int diff = dt.DayOfWeek - startOfWeek;
                if (diff < 0)
                {
                    diff += 7;
                }

                return dt.AddDays(-1 * diff).Date;
            }
    }
}
