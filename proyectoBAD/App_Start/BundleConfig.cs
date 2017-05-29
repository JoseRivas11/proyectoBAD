using System.Web;
using System.Web.Optimization;

namespace proyectoBAD
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/templateScripts").Include(
                "~/Scripts/jquery.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/metisMenu.min.js",
                "~/Scripts/sb-admin-2.min.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/templateStyles").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/metisMenu.min.css",
                "~/Content/css/sb-admin-2.min.css",
                "~/Content/css/font-awesome.min.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/dataTableScripts").Include(
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/dataTables.bootstrap.min.js",
                "~/Scripts/dataTables.responsive.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/dataTableStyles").Include(
                "~/Content/css/dataTables.bootstrap.css",
                "~/Content/css/dataTables.responsive.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/formValidationScripts").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/additional-methods.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/BootstarpUnobtrusiveValidation.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js"
                ));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }
    }
}
