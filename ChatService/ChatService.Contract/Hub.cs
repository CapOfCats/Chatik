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
        AttachmentsController attachmentsController;

        ServerEvents serverEvents;

        UserConnection userConnection {
            get => Program.connections
                .Where(c => c.connectionID == Context.ConnectionId)
                .FirstOrDefault();
        }

        public ChatHub(DBContext dbContext)
        {
            userConnectionController = new UserConnectionController();
            messageController = new MessageController(dbContext);
            chatController = new ChatController(dbContext);
            attachmentsController = new AttachmentsController(dbContext);

            serverEvents = new ServerEvents(Clients);
        }
        
        public void GetChat()
        {
            serverEvents.UpdateChat(userConnection.connectionID, chatController.GetChat(userConnection.user, userConnection.chat));
        }

        public void GetMessages(GetMessagesRequest request)
        {
            List<Message> messages = messageController
                .GetMessages(userConnection.messagesCount, request.count, userConnection.user, userConnection.chat, userConnection);
            List<int> messagesIDs = messages.Select(x => x.ID).ToList();
            List<Message> hiddenMessages = messageController
                .GetMessages(new HashSet<int>(messages.SelectMany(m => m.repliedFrom)).Where(m => messagesIDs.Contains(m)).ToList());
            List<Attachment> attachments = attachmentsController
                .GetAttachments(new HashSet<int>(messages.SelectMany(m => m.attachments)).ToList());

            userConnection.messagesCount += request.count;

            serverEvents.UpdateMessages(
                userConnection.chat,
                messages,
                hiddenMessages,
                attachments
            );
        }

        public void SendMessage(SendMessageRequest request)
        {
            messageController.AddMessage(request.text, request.repliedFrom, request.attachments, userConnection.user, userConnection.chat, userConnection);

            // TODO recalculate messagesCount for all chat connections

            // TODO use GetMessages code here
            //serverEvents.UpdateMessages(userConnection.chat);
        }

        public void DeleteMessages(DeleteMessagesRequest request)
        {
            messageController.DeleteMessages(request.IDs, userConnection.user, userConnection.chat, request.deleteForAll);

            // TODO recalculate messagesCount for all chat connections

            // TODO use GetMessages code here
            // serverEvents.UpdateMessages(userConnection.chat)
        }

        public void EditMessage(EditMessageRequest request)
        {
            messageController.EditMessage(request.id, request.text, request.attachments, request.repliedfrom, userConnection.user, userConnection.chat);

            // TODO use GetMessages code here
            // serverEvents.UpdateMessages(userConnection.chat)
        }

        public void UserTyping(UserTypingRequest request)
        {
            userConnectionController.SetTyping(Context, request.typing);

            // TODO get users
            // serverEvents.UpdateUsers(userConnection.chat, userController.getUsers(userConnection.chat));
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
