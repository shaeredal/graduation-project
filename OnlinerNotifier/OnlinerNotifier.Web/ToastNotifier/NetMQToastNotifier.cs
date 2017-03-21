using System.Linq;
using System.Net.Http;
using NetMQ;
using NetMQ.WebSockets;

namespace OnlinerNotifier.ToastNotifier
{
    public class NetMQToastNotifier : IToastNotifier
    {
        private WSPublisher publisher;

        public NetMQToastNotifier(WSPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Send(HttpRequestMessage request, string message)
        {
            var userId = GetUserId(request);
            publisher.SendMore(userId).Send(message);
        }

        public string GetUserId(HttpRequestMessage request)
        {
            return request.Headers.GetCookies("User").FirstOrDefault()?["User"].Value;
        }
    }
}