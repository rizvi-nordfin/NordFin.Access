using Microsoft.AspNet.SignalR;

namespace Nordfin
{
    public class NotificationHub : Hub
    {


        public void Send(string ClientID)
        {

            Clients.Group(ClientID).broadcastMessage("");
        }


        public void Join(string ClientID)
        {
            Groups.Add(Context.ConnectionId, ClientID);
        }
    }
}