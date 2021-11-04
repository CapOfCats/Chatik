using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СhatService.Interfaces;
using Npgsql;
using ChatService.Contract;
using Microsoft.EntityFrameworkCore;
namespace СhatService.Contract
{
    class MessageController : IMessageController
    {
        // TODO нормальное название
        ChatController ChatCon;
        private readonly DBContext dbContext;
        public MessageController(DBContext dBContext)
        {
            this.dbContext = dBContext;
            ChatCon = new ChatController(dBContext);
        }

        // TODO изменять messagesCount
        // TODO где проверка?
        public List<Message> GetMessages(int offset, int count, int user, int chatID)
        {
            Chat chat = ChatCon.GetChat(user, chatID);

            return dbContext.Messages.Where(x => chat.messages.Contains(x.ID)).Skip(offset).Take(count).ToList();
        }

        // TODO изменять messagesCount
        // TODO где проверка?
        // TODO нужен только 1 SaveChanges
        // TODO убрать Update
        public void AddMessage(string text, List<int> repliedFrom, List<int> attachments, int user, int chatID)
        {
            Chat chat = ChatCon.GetChat(user, chatID);
            var newMessage = new Message
            {
                content = text,
                author = user,
                deleted = false,
                edited = false,
                repliedFrom = repliedFrom,
                attachments = attachments
            };
            dbContext.Messages.Add(newMessage);
            dbContext.SaveChanges();
            chat.messages.Add(newMessage.ID);
            dbContext.Chats.Update(chat);
            dbContext.SaveChanges();
        }

        // TODO где проверка?
        // TODO убрать Update
        // TODO точные названия параметров
        public void EditMessage(int messageID, string text, List<int> attachments, List<int> repliedFrom, int user, int chat)
        {
            var message = dbContext.Messages.Find(messageID);
            message.content = text;
            message.attachments = attachments;
            message.repliedFrom = repliedFrom;
            message.edited = true;
            dbContext.Update(message);
            dbContext.SaveChanges();
        }

        // TODO где проверка?
        // TODO изменять messagesCount
        // TODO Поправить логику в соответствии с новым entity Message
        public void DeleteMessages(List<int> messages, int user, int chat)
        {
            dbContext.Messages.Where(x => messages.Contains(x.ID)).ToList().ForEach(x => x.deleted = true);
            dbContext.SaveChanges();
        }
    }
}