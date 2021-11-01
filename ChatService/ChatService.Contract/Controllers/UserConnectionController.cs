using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace СhatService.Contract
{
    public class UserConnectionController
    {
        public void Connect(HubCallerContext Context)
        {
            var httpContext = Context.GetHttpContext();
            Program.connections.Add(new UserConnection()
            {
                user = Convert.ToInt32(httpContext.Request.Query["user"]),
                chat = Convert.ToInt32(httpContext.Request.Query["chat"]),
                connectionID = Context.ConnectionId,
                typing = false
            });
        }
        public void Disconnect(HubCallerContext Context)
        {
            Program.connections.Remove(Program.connections.Single(c => c.connectionID == Context.ConnectionId));
        }
        public void SetTyping(HubCallerContext Context, bool isTyping)
        {
            Program.connections.Single(c => c.connectionID == Context.ConnectionId).typing = isTyping;
        }
    }
}
