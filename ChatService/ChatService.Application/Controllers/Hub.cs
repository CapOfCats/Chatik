using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using ChatService.BusinessLogic;
using СhatService.Application;

namespace СhatService.Contract
{
    public class ChatHub : Hub
    {
        UserConnectionService userConnectionService;
        MessageService messageService;
        ChatsService chatService;
        AttachmentsService attachmentsService;

        ServerEvents serverEvents;

        UserConnection userConnection {
            get => Program.connections
                .Where(c => c.connectionID == Context.ConnectionId)
                .FirstOrDefault();
        }

        public ChatHub(DBContext dbContext)
        {
            userConnectionService = new UserConnectionService();
            messageService = new MessageService(dbContext);
            chatService = new ChatsService(dbContext);
            attachmentsService = new AttachmentsService(dbContext);

            serverEvents = new Contract.ServerEvents(Clients);
        }
        
        public async Task UpdateChat()
        {
            serverEvents.UpdateChat(userConnection.connectionID, await chatService.GetChat(userConnection.user, userConnection.chat));
        }

        public async Task GetMessages(GetMessagesRequest request)
        {
            // Changelog:
            // Separation of lambda functions from variables
            // Removal of new HashSet<int>(...)

            List<Message> messages = await messageService
                .GetMessages(userConnection.messagesCount, request.count, userConnection.user, userConnection.chat, userConnection);

            // Func<T_1, T_2, ..., T_i, T_out> func_name = (var_1, var_2, ..., var_i) => Statments; where T_i is the func inputs and statements its output.
            Func<List<Message>, List<int>> getMessagesIDs = m_arr => m_arr.Select(m => m.ID).ToList();
            Func<List<Message>, List<int>> getAttachmentsIDs = m_arr => m_arr.SelectMany(m => m.attachments).ToList();
            Func<List<Message>, List<int>, List<int>> getHiddenMessagesIDs = (m_arr, id_arr) =>
                m_arr.SelectMany(m => m.repliedFrom).Where(m => id_arr.Contains(m)).ToList();

            List<int> messagesIDs = getMessagesIDs(messages);
            List<Message> hiddenMessages = await messageService.GetMessages(getHiddenMessagesIDs(messages, messagesIDs));
            List<Attachment> attachments = await attachmentsService.GetAttachments(getAttachmentsIDs(messages));

            userConnection.messagesCount += request.count;

            serverEvents.UpdateMessages(
                userConnection.chat,
                messages,
                hiddenMessages,
                attachments
            );
        }



        public async Task SendMessage(SendMessageRequest request)
        {
            await messageService.AddMessage(request.text, request.repliedFrom, request.attachments, userConnection.user, userConnection.chat, userConnection);

            
            Program.connections.Where(c => c.chat == userConnection.chat).Select(count => count.messagesCount += 1);

            List<Message> messages = 
                await messageService.GetMessages(userConnection.messagesCount, 1, userConnection.user, userConnection.chat, userConnection);
            //Add those functions to the static method!
            Func<List<Message>, List<int>> getMessagesIDs = m_arr => m_arr.Select(m => m.ID).ToList();
            Func<List<Message>, List<int>> getAttachmentsIDs = m_arr => m_arr.SelectMany(m => m.attachments).ToList();
            Func<List<Message>, List<int>, List<int>> getHiddenMessagesIDs = (m_arr, id_arr) =>
                m_arr.SelectMany(m => m.repliedFrom).Where(m => id_arr.Contains(m)).ToList();

            List<int> messagesIDs = getMessagesIDs(messages);
            List<Message> hiddenMessages = await messageService.GetMessages(getHiddenMessagesIDs(messages, messagesIDs));
            List<Attachment> attachments = await attachmentsService.GetAttachments(getAttachmentsIDs(messages));

            serverEvents.UpdateMessages(
                userConnection.chat,
                messages,
                hiddenMessages,
                attachments
            );
        }

        public async Task DeleteMessages(DeleteMessagesRequest request)
        {
            await messageService.DeleteMessages(request.IDs, userConnection.user, userConnection.chat, request.deleteForAll);

            
            Program.connections.Where(c => c.chat == userConnection.chat).Select(count => count.messagesCount += request.IDs.Count);

            List<Message> messages =
                await messageService.GetMessages(userConnection.messagesCount, 1, userConnection.user, userConnection.chat, userConnection);
            //Add those functions to the static method!
            Func<List<Message>, List<int>> getMessagesIDs = m_arr => m_arr.Select(m => m.ID).ToList();
            Func<List<Message>, List<int>> getAttachmentsIDs = m_arr => m_arr.SelectMany(m => m.attachments).ToList();
            Func<List<Message>, List<int>, List<int>> getHiddenMessagesIDs = (m_arr, id_arr) =>
                m_arr.SelectMany(m => m.repliedFrom).Where(m => id_arr.Contains(m)).ToList();

            List<int> messagesIDs = getMessagesIDs(messages);
            List<Message> hiddenMessages = await messageService.GetMessages(getHiddenMessagesIDs(messages, messagesIDs));
            List<Attachment> attachments = await attachmentsService.GetAttachments(getAttachmentsIDs(messages));

            serverEvents.UpdateMessages(
                userConnection.chat,
                messages,
                hiddenMessages,
                attachments
            );

        }

        public async Task EditMessage(EditMessageRequest request)
        {
            await messageService.EditMessage(request.id, request.text, request.attachments, request.repliedfrom, userConnection.user, userConnection.chat);

            List<Message> messages =
                await messageService.GetMessages(userConnection.messagesCount, 1, userConnection.user, userConnection.chat, userConnection);
            // TODO: add those functions to the static method
            Func<List<Message>, List<int>> getMessagesIDs = m_arr => m_arr.Select(m => m.ID).ToList();
            Func<List<Message>, List<int>> getAttachmentsIDs = m_arr => m_arr.SelectMany(m => m.attachments).ToList();
            Func<List<Message>, List<int>, List<int>> getHiddenMessagesIDs = (m_arr, id_arr) =>
                m_arr.SelectMany(m => m.repliedFrom).Where(m => id_arr.Contains(m)).ToList();

            List<int> messagesIDs = getMessagesIDs(messages);
            List<Message> hiddenMessages = await messageService.GetMessages(getHiddenMessagesIDs(messages, messagesIDs));
            List<Attachment> attachments = await attachmentsService.GetAttachments(getAttachmentsIDs(messages));

            serverEvents.UpdateMessages(
                userConnection.chat,
                messages,
                hiddenMessages,
                attachments
            );
        }

        public void UserTyping(UserTypingRequest request)
        {
            userConnectionService.SetTyping(Context, request.typing);

            //Where is UserController?
            List<User> Users = new List<User>();
            Users.Cast<User>().Select(u => u.ID == userConnection.user);

            serverEvents.UpdateUsers(userConnection.chat, Users);
        }

        public override Task OnConnectedAsync()
        {
            userConnectionService.Connect(Context);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            userConnectionService.Disconnect(Context);
            return base.OnDisconnectedAsync(exception);
        }

    }
}
