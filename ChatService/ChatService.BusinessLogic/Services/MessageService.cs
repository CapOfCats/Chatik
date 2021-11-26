using System.Collections.Generic;
using System;
using System.Linq;
using СhatService.Contract;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChatService.BusinessLogic
{
    public class MessageService : IMessageService
    {
        ChatsService chatController;
        AttachmentsService attachmentsController;
        private readonly DBContext dbContext;

        public MessageService(DBContext dBContext)
        {
            this.dbContext = dBContext;
            chatController = new ChatsService(dBContext);
            attachmentsController = new AttachmentsService(dBContext);
        }

        public async Task<List<Message>> GetMessages(int offset, int count, int userID, int chatID, UserConnection userConnection)
        {
            Chat chat = await chatController.GetChat(userID, chatID);            
            var messages = await dbContext.Messages
                .Where(x => chat.messages.Contains(x.ID))
                .Skip(offset)
                .Take(count)
                .ToListAsync();
            userConnection.messagesCount += count;
            return messages;
        }

        public async Task<List<Message>> GetMessages(List<int> IDs)
        {
            return await dbContext.Messages
                .Where(m => IDs.Contains(m.ID))
                .ToListAsync();
        }

        public async Task AddMessage(string content, List<int> repliedFrom, List<Attachment> attachments, int userID, int chatID, UserConnection userConnection)
        {
            List<int> attachmentsIDs = new List<int>();
            Chat chat = await chatController.GetChat(userID, chatID);
            if (attachments.Count != 0)
                attachmentsIDs = await attachmentsController.AddAttachments(attachments);
            var newMessage = new Message
            {
                content = content,
                author = userID,
                edited = false,
                repliedFrom = repliedFrom,
                attachments = attachmentsIDs
            };
            userConnection.messagesCount++;
            dbContext.Messages.Add(newMessage);
            chat.messages.Add(newMessage.ID);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditMessage(int ID, string content, List<Attachment> attachments, List<int> repliedFrom, int userID, int chatID)
        {
            List<int> attachmentsIDs = new List<int>();
            Chat chat = await chatController.GetChat(userID, chatID);
            Message message = dbContext.Messages
                .Where(x => chat.messages.Contains(ID))                        
                .FirstOrDefault();

            if (message == null)
                throw new Exception("Такого сообщения не существует");

            if (message.author != userID)
                throw new Exception("Не твоё - не трогай. Петух");            
            if(attachments.Count!=0)
                message.attachments = await attachmentsController.AddAttachments(attachments);
            message.content = content;
            message.repliedFrom = repliedFrom;
            message.edited = true;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteMessages(List<int> messages, int userID, int chatID, bool deleteForAll)
        {
            Chat chat = await chatController.GetChat(userID, chatID);

            dbContext.Messages
                .Where(m => messages.Contains(m.ID))
                .ToList()
                .ForEach(m => {
                    m.deletedForAll = deleteForAll;
                    if (!deleteForAll)
                    {
                        m.usersDelete.Add(userID);
                    }
                });

            await dbContext.SaveChangesAsync();
        }
    }
}