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
        
        public void UpdateChat()
        {
            serverEvents.UpdateChat(userConnection.connectionID, chatController.GetChat(userConnection.user, userConnection.chat));
        }

        public void GetMessages(GetMessagesRequest request)
        {
            // Changelog:
            // Separation of lambda functions from variables
            // Removal of new HashSet<int>(...)

            List<Message> messages = messageController
                .GetMessages(userConnection.messagesCount, request.count, userConnection.user, userConnection.chat, userConnection);

            // Func<T_1, T_2, ..., T_i, T_out> func_name = (var_1, var_2, ..., var_i) => Statments; where T_i is the func inputs and statements its output.
            Func<List<Message>, List<int>> getMessagesIDs = m_arr => m_arr.Select(m => m.ID).ToList();
            Func<List<Message>, List<int>> getAttachmentsIDs = m_arr => m_arr.SelectMany(m => m.attachments).ToList();
            Func<List<Message>, List<int>, List<int>> getHiddenMessagesIDs = (m_arr, id_arr) =>
                m_arr.SelectMany(m => m.repliedFrom).Where(m => id_arr.Contains(m)).ToList();

            List<int> messagesIDs = getMessagesIDs(messages);
            List<Message> hiddenMessages = messageController.GetMessages(getHiddenMessagesIDs(messages, messagesIDs));
            List<Attachment> attachments = attachmentsController.GetAttachments(getAttachmentsIDs(messages));

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

            
            Program.connections.Where(c => c.chat == userConnection.chat).Select(count => count.messagesCount += 1);

            List<Message> messages = 
                messageController.GetMessages(userConnection.messagesCount, 1, userConnection.user, userConnection.chat, userConnection);
            //Add those functions to the static method!
            Func<List<Message>, List<int>> getMessagesIDs = m_arr => m_arr.Select(m => m.ID).ToList();
            Func<List<Message>, List<int>> getAttachmentsIDs = m_arr => m_arr.SelectMany(m => m.attachments).ToList();
            Func<List<Message>, List<int>, List<int>> getHiddenMessagesIDs = (m_arr, id_arr) =>
                m_arr.SelectMany(m => m.repliedFrom).Where(m => id_arr.Contains(m)).ToList();

            List<int> messagesIDs = getMessagesIDs(messages);
            List<Message> hiddenMessages = messageController.GetMessages(getHiddenMessagesIDs(messages, messagesIDs));
            List<Attachment> attachments = attachmentsController.GetAttachments(getAttachmentsIDs(messages));

            serverEvents.UpdateMessages(
                userConnection.chat,
                messages,
                hiddenMessages,
                attachments
            );
        }

        public void DeleteMessages(DeleteMessagesRequest request)
        {
            messageController.DeleteMessages(request.IDs, userConnection.user, userConnection.chat, request.deleteForAll);

            
            Program.connections.Where(c => c.chat == userConnection.chat).Select(count => count.messagesCount += request.IDs.Count);

            List<Message> messages =
                messageController.GetMessages(userConnection.messagesCount, 1, userConnection.user, userConnection.chat, userConnection);
            //Add those functions to the static method!
            Func<List<Message>, List<int>> getMessagesIDs = m_arr => m_arr.Select(m => m.ID).ToList();
            Func<List<Message>, List<int>> getAttachmentsIDs = m_arr => m_arr.SelectMany(m => m.attachments).ToList();
            Func<List<Message>, List<int>, List<int>> getHiddenMessagesIDs = (m_arr, id_arr) =>
                m_arr.SelectMany(m => m.repliedFrom).Where(m => id_arr.Contains(m)).ToList();

            List<int> messagesIDs = getMessagesIDs(messages);
            List<Message> hiddenMessages = messageController.GetMessages(getHiddenMessagesIDs(messages, messagesIDs));
            List<Attachment> attachments = attachmentsController.GetAttachments(getAttachmentsIDs(messages));

            serverEvents.UpdateMessages(
                userConnection.chat,
                messages,
                hiddenMessages,
                attachments
            );

        }

        public void EditMessage(EditMessageRequest request)
        {
            messageController.EditMessage(request.id, request.text, request.attachments, request.repliedfrom, userConnection.user, userConnection.chat);

            List<Message> messages =
                messageController.GetMessages(userConnection.messagesCount, 1, userConnection.user, userConnection.chat, userConnection);
            //Add those functions to the static method!
            Func<List<Message>, List<int>> getMessagesIDs = m_arr => m_arr.Select(m => m.ID).ToList();
            Func<List<Message>, List<int>> getAttachmentsIDs = m_arr => m_arr.SelectMany(m => m.attachments).ToList();
            Func<List<Message>, List<int>, List<int>> getHiddenMessagesIDs = (m_arr, id_arr) =>
                m_arr.SelectMany(m => m.repliedFrom).Where(m => id_arr.Contains(m)).ToList();

            List<int> messagesIDs = getMessagesIDs(messages);
            List<Message> hiddenMessages = messageController.GetMessages(getHiddenMessagesIDs(messages, messagesIDs));
            List<Attachment> attachments = attachmentsController.GetAttachments(getAttachmentsIDs(messages));

            serverEvents.UpdateMessages(
                userConnection.chat,
                messages,
                hiddenMessages,
                attachments
            );
        }

        public void UserTyping(UserTypingRequest request)
        {
            userConnectionController.SetTyping(Context, request.typing);

            //Where is UserController?
            List<User> Users = new List<User>();
            Users.Cast<User>().Select(u => u.ID == userConnection.user);

            serverEvents.UpdateUsers(userConnection.chat, Users);
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
