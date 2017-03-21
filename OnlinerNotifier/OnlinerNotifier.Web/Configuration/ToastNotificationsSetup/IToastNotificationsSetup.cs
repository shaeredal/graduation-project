using Autofac;
using Owin;

namespace OnlinerNotifier.Configuration.ToastNotificationsSetup
{
    public interface IToastNotificationsSetup
    {
        string NotificationsModuleName { get; }
        string NotificationsBundleName { get; }
        void RegisterDependencies(ContainerBuilder builder);
        void StartupConfiguration(IAppBuilder app);
    }
}
