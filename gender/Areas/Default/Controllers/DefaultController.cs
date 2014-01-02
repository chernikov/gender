using System.Linq;
using System.Web.Routing;
using System.Globalization;
using System.Threading;
using System.Resources;

using gender.Controllers;
using gender.Global;
using gender.Helpers;
using gender.Model;

namespace gender.Areas.Default.Controllers
{
    public abstract class DefaultController : BaseController
    {

        public static int ModerateRating = 5;
    }
}
