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
            MC = new MessageController(dbContext);
            CC = new ChatController(dbContext);

            // test
            dbContext.Chats.Add(new Chat { title = "hello db" });
            dbContext.SaveChanges();
            Console.WriteLine(dbContext.Chats.ToList()[0].title);
        }
        
        public void GetChat(int id)
        {  
           // foreach(UserConnection connection in Program.connections)
             // chat = CC.GetChat(connection.user, id); Нужно объявление чата. И каким юзерам выдавать чат,а каким - нет? Всем - тоже не вариант. 
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateChat", id);
        }
        public void GetMessages(int offset, int count)
        {
            //List<Message> messes = MC.GetMessages(offset, count, conn.user, conn.chat);//та же муйня
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateMessages", offset, count);         
        }
        void SendMessage(string text, int[] repliedFrom, int[] attachments)
        {
            //MC.AddMessage(text, repliedFrom, attachments, conn.user, conn.chat);
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateMessages", text, repliedFrom, attachments);
        }
        void DeleteMessages(int[] IDs)
        {
            // MC.DeleteMessages(IDs, conn.user, conn.chat);
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateMessages", IDs);
        }
        void EditMessage(int id, string text, int[] attachments, int[] repliedfrom)
        {
            // MC.EditMessage(id, text, attachments, repliedfrom, conn.user, conn.chat);
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
