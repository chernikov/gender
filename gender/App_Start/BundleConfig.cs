using System.Web;
using System.Web.Optimization;

namespace gender
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-1.*", 
                      "~/Scripts/jquery-ui-1.*",
                      "~/Scripts/jquery.blockUI*"));
            bundles.Add(new ScriptBundle("~/bundles/fineuploader").Include(
                      "~/Scripts/jquery.fineuploader*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                   "~/Scripts/bootstrap*"
                   ));
            bundles.Add(new ScriptBundle("~/bundles/theme").Include(
                      "~/Scripts/bootstrap*",
                      "~/Scripts/lightbox.js",
                      "~/Scripts/prettify.js",
                      "~/Scripts/main.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/chosen").Include(
                   "~/Scripts/chosen*", "~/Scripts/ajax-chosen*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/css/reset.css", "~/Content/css/main.css"));
            bundles.Add(new StyleBundle("~/Content/css/admin").Include("~/Content/css/admin.css", "~/Content/css/jquery-ui-*"));
            bundles.Add(new StyleBundle("~/Content/css/jquery-ui").Include("~/Content/css/jquery-ui-*", "~/Content/css/datepicker*"));
            bundles.Add(new StyleBundle("~/Content/css/fineuploader").Include("~/Content/css/fineuploader.css"));

            bundles.Add(new StyleBundle("~/Content/css/bootstrap").Include(
                "~/Content/css/bootstrap*"));
            bundles.Add(new StyleBundle("~/Content/css/theme").Include(
                "~/Content/css/gender.css"));
            bundles.Add(new StyleBundle("~/Content/css/chosen").Include("~/Content/css/chosen*"));
        }
    }
}