using Microsoft.AspNet.SignalR;

namespace OnlinerNotifier.Hubs
{
    public class ToastHub : Hub
    {
        public void GetUserId()
        {
            Clients.Caller.setUserId(Context.ConnectionId);
        }
    }
}