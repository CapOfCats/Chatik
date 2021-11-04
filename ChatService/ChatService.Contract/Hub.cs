using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatService.Contract;
using Microsoft.AspNetCore.SignalR;
using System.Linq;


namespace СhatService.Contract
{
    public class ChatHub : Hub
    {
        
        UserConnectionController userConnectionController;
        MessageController messageController;
        ChatController chatController;

        private readonly DBContext dbContext;

        public ChatHub(DBContext dbContext)
        {
            this.dbContext = dbContext;
            userConnectionController = new UserConnectionController();
            messageController = new MessageController(dbContext);
            chatController = new ChatController(dbContext);
        }
        
        public void GetChat()
        {
            UserConnection userConnection = Program.connections
                .Where(c => c.connectionID == Context.ConnectionId)
                .FirstOrDefault();
            chatController.GetChat(userConnection.user, userConnection.chat);
            Clients.Clients(
                this.Context.ConnectionId)
                .SendAsync("UpdateChat");
        }
        public void GetMessages(int offset, int count)
        {
            UserConnection userConnection = Program.connections
                .Where(c => c.connectionID == Context.ConnectionId)
                .FirstOrDefault();
            //userConnection.messagesCount += count;
            /*List<Message> messes =*/ messageController.GetMessages(offset, count, userConnection.user, userConnection.chat,userConnection);
            Clients.Clients(
                this.Context.ConnectionId)
                .SendAsync("UpdateMessages", offset, count);         
        }
       public void SendMessage(string text, List<int> repliedFrom, List<Attachment> attachments)
        {
            UserConnection userConnection = Program.connections
                .Where(c => c.connectionID == Context.ConnectionId)
                .FirstOrDefault();
            messageController.AddMessage(text, repliedFrom, attachments, userConnection.user, userConnection.chat,userConnection);
            //userConnection.messagesCount++;
            Clients.Clients(
                this.Context.ConnectionId)
                .SendAsync("UpdateMessages", text, repliedFrom, attachments);
        }
        public void DeleteMessages(List<int> IDs, bool deleteAll)
        {
            UserConnection userConnection = Program.connections
                .Where(c => c.connectionID == Context.ConnectionId)
                .FirstOrDefault();
            messageController.DeleteMessages(IDs, userConnection.user, userConnection.chat, userConnection, deleteAll);
            Clients.Clients(
                this.Context.ConnectionId)
                .SendAsync("UpdateMessages", IDs);
        }
        public void EditMessage(int id, string text, List<Attachment> attachments, List<int> repliedfrom)
        {
            UserConnection userConnection = Program.connections
                .Where(c => c.connectionID == Context.ConnectionId)
                .FirstOrDefault();
            messageController.EditMessage(id, text, attachments, repliedfrom, userConnection.user, userConnection.chat);
            Clients.Clients(
                this.Context.ConnectionId)
                .SendAsync("UpdateMessages", id, text, attachments, repliedfrom);
        }
       public void UserTyping(bool typing)
        {
            userConnectionController.SetTyping(Context, typing);
            Clients.Clients(
                this.Context.ConnectionId)
                .SendAsync("UpdateUser", typing);
        }

        public override Task OnConnectedAsync()
        {
            userConnectionController.Connect(Context);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            userConnectionController.Disconnect(Context);
            return base.OnDisconnectedAsync(exception);
        }

    }
}
