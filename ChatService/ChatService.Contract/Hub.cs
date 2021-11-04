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
        // TODO camelCase названия для экземпляров
        UserConnectionController userconnectioncontroller;
        MessageController messagecontroller;
        ChatController chatcontroller;

        private readonly DBContext dbContext;

        public ChatHub(DBContext dbContext)
        {
            this.dbContext = dbContext;
            userconnectioncontroller = new UserConnectionController();
            messagecontroller = new MessageController(dbContext);
            chatcontroller = new ChatController(dbContext);

            // TODO remove test
            // test
            dbContext.Chats.Add(new Chat { title = "hello db" });
            dbContext.SaveChanges();
            Console.WriteLine(dbContext.Chats.ToList()[0].title);
        }

        // TODO использовать ServerEvents

        // TODO убрать id
        public void GetChat(int id)
        {
            UserConnection userConnection = Program.connections
                .Where(c => c.connectionID == Context.ConnectionId)
                .FirstOrDefault();
            chatcontroller.GetChat(userConnection.user, userConnection.chat);
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateChat", id);
        }

        public void GetMessages(int offset, int count)
        {
            UserConnection userConnection = Program.connections
                .Where(c => c.connectionID == Context.ConnectionId)
                .FirstOrDefault();
            /*List<Message> messes =*/ messagecontroller.GetMessages(offset, count, userConnection.user, userConnection.chat);
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateMessages", offset, count);         
        }

        // TODO сделать методы public
        void SendMessage(string text, List<int> repliedFrom, List<int> attachments)
        {
            UserConnection userConnection = Program.connections
                .Where(c => c.connectionID == Context.ConnectionId)
                .FirstOrDefault();
            messagecontroller.AddMessage(text, repliedFrom, attachments, userConnection.user, userConnection.chat);
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateMessages", text, repliedFrom, attachments);
        }
        void DeleteMessages(List<int> IDs)
        {
            UserConnection userConnection = Program.connections
                .Where(c => c.connectionID == Context.ConnectionId)
                .FirstOrDefault();
            messagecontroller.DeleteMessages(IDs, userConnection.user, userConnection.chat);
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateMessages", IDs);
        }

        // TODO attachments - массив объектов {name, src}
        void EditMessage(int id, string text, List<int> attachments, List<int> repliedfrom)
        {
            UserConnection userConnection = Program.connections
                .Where(c => c.connectionID == Context.ConnectionId)
                .FirstOrDefault();
            messagecontroller.EditMessage(id, text, attachments, repliedfrom, userConnection.user, userConnection.chat);
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateMessages", id, text, attachments, repliedfrom);
        }
        void UserTyping(bool typing)
        {
            userconnectioncontroller.SetTyping(Context, typing);
            Clients.Clients(this.Context.ConnectionId).SendAsync("UpdateUser", typing);
        }

        public override Task OnConnectedAsync()
        {
            userconnectioncontroller.Connect(Context);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            userconnectioncontroller.Disconnect(Context);
            return base.OnDisconnectedAsync(exception);
        }

    }
}
