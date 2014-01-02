using gender.Tools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace gender.UnitTest.OtherTest
{
    [TestFixture]
    public class SearchTest
    {
        [Test]
        public void Search()
        {
            var text = "Северная Америка в начале XXI века: женщины и политика. Гендерная политическая культура : науч. докл.";
            var SearchString = "Женщины и политика";
            var term = SearchString.ToLowerInvariant().Trim(); //StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Trim();//.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));
            int rank = 0;
            rank += Regex.Matches(text.ToLowerInvariant(), regex).Count;

            Assert.AreEqual(1, rank);
        }
    }
}
