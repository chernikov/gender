using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gender.Tools;

namespace gender.UnitTest.OtherTest
{
    [TestFixture]
    public class DateTimeTest
    {
        [Test]
        public void TestStartDate()
        {
            var date = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var date2 = DateTime.Now.StartOfWeek(DayOfWeek.Thursday);

            var date3 = DateTime.Now.AddDays(3).StartOfWeek(DayOfWeek.Thursday);
        }
    
    }
}
