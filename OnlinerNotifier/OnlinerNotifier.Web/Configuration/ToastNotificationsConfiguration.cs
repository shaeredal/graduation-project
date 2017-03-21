using System.Configuration;

namespace OnlinerNotifier.Configuration
{
    public class ToastNotificationsConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("notificationsProviderType", IsRequired = true, DefaultValue = "SignalR")]
        public string NotificationsProviderType
        {
            get
            {
                return (string) this["notificationsProviderType"];
            }
            set
            {
                this["notificationsProviderType"] = value;
            }
        }
    }
}