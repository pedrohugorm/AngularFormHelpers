using System.Web.Optimization;

namespace AngularFormHelpers
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/bower_components/jquery/dist/jquery.js",
                "~/bower_components/bootstrap/dist/js/bootstrap.js",
                "~/bower_components/angular/angular.js",
                "~/bower_components/angular-resource/angular-resource.js",
                "~/bower_components/angular-input-masks/angular-input-masks-standalone.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/bower_components/bootstrap/css/bootstrap.css"
                ));
        }
    }
}
