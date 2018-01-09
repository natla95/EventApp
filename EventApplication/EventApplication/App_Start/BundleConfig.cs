using System.Web;
using System.Web.Optimization;

namespace EventApplication
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // scripts
            bundles.Add(new ScriptBundle("~/bundles/account_js").Include(
               "~/Scripts/external/jquery.min.js",
               "~/Scripts/custom/customAccount.js"
             ));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
               "~/Scripts/external/jquery.min.js",
               "~/Scripts/custom/customMain.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/datepicker_js").Include(
                "~/Scripts/external/jquery-ui-1.12.1.min.js",
                "~/Scripts/custom/dataPicker.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/forms_js").Include(
              "~/Scripts/custom/customForms.js"
              ));

            // styles 
            bundles.Add(new StyleBundle("~/Content/main_syle").Include(
                "~/Content/style/mainStyle.min.css"
             ));

            bundles.Add(new StyleBundle("~/Content/organizer_style").Include(
                 "~/Content/style/organizerMainPageStyle.min.css"
             ));

            bundles.Add(new StyleBundle("~/Content/table_style").Include(
                "~/Content/style/mainStyle.min.css",
                "~/Content/style/tableDataStyle.min.css"
            ));

            bundles.Add(new StyleBundle("~/Content/date_picker_css").Include(
                "~/Content/themes/datepicker.css",
                "~/Content/themes/jquery-ui.min.css",
                "~/Content/css/bootstrapDatePick.min.css",
                "~/Content/css/bootstrap-datetimepicker.min.css"
          ));
        }
    }
}
