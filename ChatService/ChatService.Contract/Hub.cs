using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChatService.Contract;
using Microsoft.AspNetCore.SignalR;
using System.Linq;


namespace СhatService.Contract
{
    public class ChatHub : Hub
    {
        UserConnectionController UC;
        MessageController MC;
        ChatController CC;

        private readonly DBContext dbContext;

        public ChatHub(DBContext dbContext)
        {
            this.dbContext = dbContext;
            UC = new UserConnectionController();
            MC = new MessageController();
            CC = new ChatController();

            // test
            dbContext.Chats.Add(new Chat { title = "hello db" });
            dbContext.SaveChanges();
            Console.WriteLine(dbContext.Chats.ToList()[0].title);
        }

        public void GetChat(int id)
        {
            //var ch = ChatController.GetChat(conn.user, id);
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateChat", id);
        }
        public void GetMessages(int offset, int count)
        {
            //Message[] messes = MessageController.GetMessages(offset, count, conn.user, conn.chat);
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateMessages", offset, count);         
        }
        void SendMessage(string text, int[] repliedFrom, int[] attachments)
        {
            //MessageController.AddMessage();
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateMessages", text, repliedFrom, attachments);
        }
        void DeleteMessages(int[] IDs)
        {
            //MC.DeleteMessages();
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateMessages", IDs);
        }
        void EditMessage(int id, string text, int[] attachments, int[] repliedfrom)
        {
            //MC.EditMessage();
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateMessages", id, text, attachments, repliedfrom);
        }
        void UserTyping(bool typing)
        {
            UC.SetTyping(Context, typing);
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateUser", typing);
        }

        public override Task OnConnectedAsync()
        {
            UC.Connect(Context);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            UC.Disconnect(Context);
            return base.OnDisconnectedAsync(exception);
        }

    }
}
