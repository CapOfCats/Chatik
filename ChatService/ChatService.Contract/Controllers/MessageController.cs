using System.Collections.Generic;
using System;
using System.Linq;
using СhatService.Interfaces;
using ChatService.Contract;

namespace СhatService.Contract
{
    class MessageController : IMessageController
    {
        ChatController chatController;
        private readonly DBContext dbContext;

        public MessageController(DBContext dBContext)
        {
            this.dbContext = dBContext;
            chatController = new ChatController(dBContext);
        }

        public List<Message> GetMessages(int offset, int count, int userID, int chatID, UserConnection userConnection)
        {
            Chat chat = chatController.GetChat(userID, chatID);            
            var messages = dbContext.Messages
                .Where(x => chat.messages.Contains(x.ID))
                .Skip(offset)
                .Take(count)
                .ToList();
            userConnection.messagesCount += count;
            return messages;          
        }
        public void AddMessage(string content, List<int> repliedFrom, List<Attachment> attachments, int userID, int chatID, UserConnection userConnection)
        {
            Chat chat = chatController.GetChat(userID, chatID);
            var newMessage = new Message
            {
                content = content,
                author = userID,
                edited = false,
                repliedFrom = repliedFrom,
                attachments = attachments
            };
            userConnection.messagesCount++;
            dbContext.Messages.Add(newMessage);
            chat.messages.Add(newMessage.ID);
            dbContext.SaveChanges();
        }
        public void EditMessage(int ID, string content, List<Attachment> attachments, List<int> repliedFrom, int userID, int chatID)
        {
            Chat chat = chatController.GetChat(userID, chatID);
            var message = dbContext.Messages
                .Where(x => chat.messages.Contains(ID))                        
                .FirstOrDefault();
            if (message == null)
                throw new Exception("Такого сообщения не существует");
            else 
            if (message.author != userID)
                throw new Exception("Не твоё-не трогай. Петух.");
            else
            {
                message.content = content;
                message.attachments = attachments;
                message.repliedFrom = repliedFrom;
                message.edited = true;
                dbContext.SaveChanges();
            }
        }
        public void DeleteMessages(List<int> messages, int userID, int chatID, UserConnection userConnection, bool deleteAll)
        {
            Chat chat = chatController.GetChat(userID, chatID);
            int count = dbContext.Messages
                    .Count(x => chat.messages.Contains(x.ID) && messages.Contains(x.ID));
            if (!deleteAll)
                 dbContext.Messages
                    .Where(x => chat.messages.Contains(x.ID) &&  messages.Contains(x.ID))
                    .ToList()
                    .ForEach(x => x.usersDelete.Add(userID));
            else
            {
                dbContext.Messages
                .Where(x => chat.messages.Contains(x.ID) && messages.Contains(x.ID))
                .ToList()
                .ForEach(x => x.deletedForAll = true);
            }
            userConnection.messagesCount -= count;
            dbContext.SaveChanges();
        }
    }
}