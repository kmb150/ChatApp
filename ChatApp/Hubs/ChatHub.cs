using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string who, string message)
        {
            string name = Context.User.Identity.Name;
            Clients.Group(name).appendNewMessage(name, message);
            Clients.Group(who).appendNewMessage(name, message);
           
        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;

            Groups.Add(Context.ConnectionId, name);

            return base.OnConnected();
        }
    }
}