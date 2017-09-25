using System.Web.Optimization;

namespace FriendsMatrix.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/Libs/angular/angular.js",
                "~/Scripts/Libs/angular-resource/angular-resource.js",
                "~/Scripts/Libs/angular-material/angular-animate.js",
                "~/Scripts/Libs/angular-material/angular-aria.js",
                "~/Scripts/Libs/angular-material/angular-material.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app/app.js",
                "~/Scripts/app/Services/*.js",
                "~/Scripts/app/Model/*.js",
                "~/Scripts/app/Controllers/*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/*.css"));
        }
    }
}