using Autofac;
using OnlinerNotifier.ToastNotifier;
using Owin;

namespace OnlinerNotifier.Configuration.ToastNotificationsSetup
{
    public class SignalRNotificationsSetup : IToastNotificationsSetup
    {
        public string NotificationsModuleName => "signalRToastNotifications";

        public string NotificationsBundleName => "~/bundles/SignalR";

        public void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<SignalRToastNotifier>().As<IToastNotifier>();
        }

        public void StartupConfiguration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}