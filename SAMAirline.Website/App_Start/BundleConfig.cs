using System.Web;
using System.Web.Optimization;

namespace SAMAirline.Website
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Style/js/jquery*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Style/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Style/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Style/js/jquery.validate*"));
        }
    }
}
