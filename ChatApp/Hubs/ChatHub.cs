using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ChatApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string who, string message)
        {
            string name = Context.User.Identity.Name;
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            var currentUser = manager.FindByEmail(who);
            string receiverId = currentUser.Id;

            
            if (who!="")
            {
                Message messageObj = new Message(message,DateTime.Now,Context.User.Identity.GetUserId(),receiverId);
                MessagesContext db = new MessagesContext();
                messageObj.AddMessage(db);
                Clients.Group(name).appendNewMessage(name, message, messageObj.GetFormattedDate());
                Clients.Group(who).appendNewMessage(name, message, messageObj.GetFormattedDate());
            }
            

        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            Groups.Add(Context.ConnectionId, name);
            return base.OnConnected();
        }
    }
}