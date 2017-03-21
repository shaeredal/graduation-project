using System.Web.Optimization;

namespace OnlinerNotifier
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/scripts/angular.js")
                .Include("~/scripts/angular-route.js")
                .Include("~/scripts/angular-cookies.js")
                .Include("~/scripts/angular-ui/ui-bootstrap.js")
                .Include("~/scripts/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(new ScriptBundle("~/bundles/ng-infinite-scroll")
                .Include("~/scripts/ng-infinite-scroll.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr")
                .Include("~/scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/app/app.js")
                .Include("~/app/controllers/authController.js")
                .Include("~/app/controllers/accountController.js")
                .Include("~/app/controllers/homeController.js")
                .Include("~/app/filters/trackingFormatFilter.js")
                .Include("~/app/filters/currencyFilter.js")
                .Include("~/app/filters/notificationFilter.js")
                .Include("~/app/services/currencyService.js"));

            bundles.Add(new ScriptBundle("~/bundles/SignalR")
                .Include("~/scripts/jquery.signalR-{version}.js")
                .Include("~/app/other/signalRToastNotifications.js"));

            bundles.Add(new ScriptBundle("~/bundles/NetMQ")
                .Include("~/scripts/JSMQ.js")
                .Include("~/app/other/netMQToastNotifications.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/ui-bootstrap-csp.css"));

            bundles.Add(new StyleBundle("~/Content/font-awesome")
                .Include("~/Content/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/toastr")
                .Include("~/Content/toastr.css"));

            bundles.Add(new StyleBundle("~/Content/site")
                .Include("~/Content/app/site.css")
                .Include("~/Content/app/auth.css")
                .Include("~/Content/app/home.css")
                .Include("~/Content/app/account.css"));
        }
    }
}