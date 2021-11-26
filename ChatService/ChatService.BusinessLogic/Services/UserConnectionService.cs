using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using СhatService.Contract;
using System.Threading.Tasks;

namespace ChatService.BusinessLogic
{
    public class UserConnectionService
    {
        public void Connect(HubCallerContext Context)
        {
            var httpContext = Context.GetHttpContext();
            try
            {
                Program.connections.Add(new UserConnection()
                {
                    user = Convert.ToInt32(httpContext.Request.Query["user"]),
                    chat = Convert.ToInt32(httpContext.Request.Query["chat"]),
                    connectionID = Context.ConnectionId,
                });
            }
            catch (Exception error) {
                Console.WriteLine(error.Message);
            }
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
