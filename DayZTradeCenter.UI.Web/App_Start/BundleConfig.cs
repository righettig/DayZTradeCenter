using System.Web.Optimization;

namespace DayZTradeCenter.UI.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/highcharts").Include(
                "~/Scripts/Highcharts-4.0.1/js/highcharts.js"));

            bundles.Add(new ScriptBundle("~/bundles/landing").Include(
                "~/Scripts/app/latesttrades.js",
                "~/Scripts/app/landing.js"));

            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                "~/Scripts/app/latesttrades.js",
                "~/Scripts/app/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                "~/Scripts/app/_layout.js"));

            bundles.Add(new ScriptBundle("~/bundles/trades_create").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/app/CreateTradeViewModel.js",
                "~/Content/Selectize/js/standalone/selectize.js",
                "~/Scripts/ko_selectize.js"));

            bundles.Add(new ScriptBundle("~/bundles/trades_index").Include(
                "~/Content/Selectize/js/standalone/selectize.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/alertify.js",
                "~/Scripts/app/trades_index.js"));

            bundles.Add(new ScriptBundle("~/bundles/items_index").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/alertify.js",
                "~/Scripts/app/items_index.js"));

            bundles.Add(new ScriptBundle("~/bundles/inbox").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/moment.js",
                "~/Scripts/alertify.js",
                "~/Scripts/app/InboxViewModel.js"));

            bundles.Add(new ScriptBundle("~/bundles/exchange").Include(
                "~/Scripts/moment.js",
                "~/Scripts/bootstrap-datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/tradecompleted").Include(
                "~/Scripts/app/tradecompleted.js"));

            #endregion
            
            #region Styles

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));

            // Bugfix: odd behaviour when the bundle path is the same as an actual path.
            // http://stackoverflow.com/a/17725655/3265316
            bundles.Add(new StyleBundle("~/Content/alertify").Include(
                "~/Content/alertifyjs/alertify.css",
                "~/Content/alertifyjs/themes/default.css"));

            bundles.Add(new StyleBundle("~/Content/pagedlist").Include(
                "~/Content/PagedList.css"));

            #endregion

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
