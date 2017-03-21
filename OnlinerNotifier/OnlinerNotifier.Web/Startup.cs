using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(OnlinerNotifier.Startup))]

namespace OnlinerNotifier
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ToastNotificationsConfig.Setup.StartupConfiguration(app);
        }
    }
}
