using System.Web.Optimization;

namespace TableTopTally
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jQuery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jQuery/jquery.validate*"));

            // Drew added this - Note: No idea if it works or is proper!
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/Angular/angular.js",
                        "~/Scripts/Angular/angular-animate.js",
                        "~/Scripts/Angular/angular-cookies.js",
                        "~/Scripts/Angular/angular-loader.js",
                        "~/Scripts/Angular/angular-resource.js",
                        "~/Scripts/Angular/angular-route.js",
                        "~/Scripts/Angular/angular-sanitize.js",
                        "~/Scripts/Angular/angular-touch.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.js",
                      "~/Scripts/respond/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true;
        }
    }
}
