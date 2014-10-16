using System.Web.Optimization;

namespace DayZTradeCenter.UI.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
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
                        "~/Scripts/app/trades_index.js"));

            bundles.Add(new ScriptBundle("~/bundles/items_index").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/alertify.min.js",
                        "~/Scripts/app/items_index.js"));

            bundles.Add(new ScriptBundle("~/bundles/exchange").Include(
                        "~/Scripts/moment.js",
                        "~/Scripts/bootstrap-datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/tradecompleted").Include(
                        "~/Scripts/app/tradecompleted.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
