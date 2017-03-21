using System.Net.Http;

namespace OnlinerNotifier.ToastNotifier
{
    public interface IToastNotifier
    {
        void Send(HttpRequestMessage request, string message);
    }
}
