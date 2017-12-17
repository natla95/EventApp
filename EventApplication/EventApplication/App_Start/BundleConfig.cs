using System.Web;
using System.Web.Optimization;

namespace EventApplication
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/account_js").Include(
               "~/Scripts/external/jquery.min.js",
               "~/Scripts/custom/customAccount.js"
             ));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
               "~/Scripts/external/jquery.min.js",
               "~/Scripts/custom/customMain.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/datepicker_js").Include(
                "~/Scripts/external/jquery-ui-1.12.1.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/forms_js").Include(
              "~/Scripts/custom/customForms.js"
              ));


            bundles.Add(new StyleBundle("~/Content/date_picker_css").Include(
                "~/Content/themes/jquery-ui.min.css"
         ));
        }
    }
}
