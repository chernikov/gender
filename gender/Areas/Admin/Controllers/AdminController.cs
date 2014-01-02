using System.Linq;
using gender.Controllers;
using gender.Model;
using System.Web.Routing;
using System.Globalization;
using System.Threading;

namespace gender.Areas.Admin.Controllers
{

    public abstract class AdminController : BaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            CultureInfo ci = new CultureInfo("ru");

            Thread.CurrentThread.CurrentCulture = ci;
            base.Initialize(requestContext);
        }

    }
}
