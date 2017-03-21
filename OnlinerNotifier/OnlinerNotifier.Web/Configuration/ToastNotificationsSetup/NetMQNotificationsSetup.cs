using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using NetMQ;
using NetMQ.WebSockets;
using OnlinerNotifier.ToastNotifier;
using Owin;

namespace OnlinerNotifier.Configuration.ToastNotificationsSetup
{
    public class NetMqNotificationsSetup : IToastNotificationsSetup
    {
        public string NotificationsModuleName => "netMQToastNotifications";

        public string NotificationsBundleName => "~/bundles/NetMQ";

        public void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterInstance(NetMQContext.Create().CreateWSPublisher()).SingleInstance();
            builder.RegisterType<NetMQToastNotifier>().As<IToastNotifier>();
        }

        public void StartupConfiguration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.DependencyResolver
                .GetRootLifetimeScope().Resolve<WSPublisher>().Bind("ws://localhost:81");
        }
    }
}