using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
using gender.Global.Config;

namespace gender.UnitTest
{

    [TestFixture]
    public class MimeTypesTest
    {
        [Test]
        public void MimeTypes_AreManyMimeTypesExist_ManyMimeTypesExist()
        {
            var config = DependencyResolver.Current.GetService<IConfig>();
            var count = config.MimeTypes.Count();
            Assert.Greater(count, 0);
        }
    }
}
