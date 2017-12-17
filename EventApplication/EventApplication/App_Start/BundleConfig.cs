using System.Web;
using System.Web.Optimization;

namespace EventApplication
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
               "~/Scripts/external/jquery.min.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                "~/Scripts/external/jquery-1.12.4.js",
                "~/Scripts/external/jquery-ui.js",
                "~/Scripts/external/bootstrap-datetimepicker.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/forms_js").Include(
              "~/Scripts/custom/customForms.js"
              ));


            bundles.Add(new StyleBundle("~/Content/date_picker").Include(
              "~/Content/css/jquery-ui.css"
         ));
        }
    }
}
