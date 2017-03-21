using System.Linq;
using System.Net.Http;
using Microsoft.AspNet.SignalR;
using OnlinerNotifier.Hubs;

namespace OnlinerNotifier.ToastNotifier
{
    public class SignalRToastNotifier : IToastNotifier
    {
        public void Send(HttpRequestMessage request, string message)
        {
            var connectionId = GetSignalRConnectionId(request);
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ToastHub>();
            hubContext.Clients.Client(connectionId).notify(message);
        }

        private string GetSignalRConnectionId(HttpRequestMessage request)
        {
            return request.Headers.GetCookies("signalRUserId").FirstOrDefault()?["signalRUserId"].Value;
        }
    }
}