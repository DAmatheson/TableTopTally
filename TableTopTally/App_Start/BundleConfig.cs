using System.Web.Optimization;

namespace TableTopTally
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Library/jQuery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Library/jQueryValidate/jquery.validate*"));

            // Drew added this - Note: No idea if it works or is proper!
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/Library/Angular/angular.js",
                        //"~/Scripts/Library/Angular/angular-animate.js",
                        //"~/Scripts/Library/Angular/angular-cookies.js",
                        //"~/Scripts/Library/Angular/angular-loader.js",
                        "~/Scripts/Library/Angular/angular-resource.js",
                        "~/Scripts/Library/Angular/angular-route.js"
                        //"~/Scripts/Library/Angular/angular-sanitize.js",
                        //"~/Scripts/Library/Angular/angular-touch.js"
                        ).
                        IncludeDirectory("~/Scripts/Angular", "*.js", true));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Library/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Library/bootstrap/bootstrap.js",
                      "~/Scripts/Library/respond/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true;
        }
    }
}
