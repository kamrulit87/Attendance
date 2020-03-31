using System.Web;
using System.Web.Optimization;

namespace IASystemWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery-top").Include(
            //            "~/Scripts/jquery.{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Content/built_in/assets/plugins/modernizr.custom.js",
                "~/Content/built_in/assets/plugins/jquery-ui/jquery-ui.min.js",
                "~/Content/built_in/assets/plugins/boostrapv3/js/bootstrap.min.js",
                "~/Content/built_in/assets/plugins/jquery-scrollbar/jquery.scrollbar.min.js",
                "~/Content/built_in/assets/plugins/classie/classie.js",
                "~/Content/built_in/assets/plugins/jquery-validation/js/jquery.validate.min.js",
                "~/Content/built_in/assets/js/form_layouts.js",
                "~/Content/built_in/assets/js/scripts.js",
                "~/Content/built_in/assets/js/demo.js",
                "~/Content/built_in/assets/js/form_elements.js",
                "~/Content/user_define/js/message/jquery-impromptu.js"
                        ));

            //bundles.Add(new ScriptBundle("~/bundles/angular").Include(
            //            "~/Content/angular/angular.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                        "~/Content/built_in/assets/plugins/boostrapv3/css/bootstrap.min.css",
                        "~/Content/built_in/assets/plugins/jquery-scrollbar/jquery.scrollbar.css",
                        "~/Content/user_define/js/message/jquery-impromptu.css"
                       ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
