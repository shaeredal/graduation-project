using System.Configuration;
using OnlinerNotifier.Configuration;
using OnlinerNotifier.Configuration.ToastNotificationsSetup;

namespace OnlinerNotifier
{
    public class ToastNotificationsConfig
    {
        public static IToastNotificationsSetup Setup { get; private set; }

        public static void SetProviderSetup()
        {
            var section = (ToastNotificationsConfiguration) ConfigurationManager.GetSection("toastNotifications");
            switch (section.NotificationsProviderType)
            {
                case "SignalR":
                    Setup = new SignalRNotificationsSetup();
                    break;
                case "NetMQ":
                    Setup = new NetMqNotificationsSetup();
                    break;
                default:
                    throw new ConfigurationErrorsException();
            }
        }
    }
}