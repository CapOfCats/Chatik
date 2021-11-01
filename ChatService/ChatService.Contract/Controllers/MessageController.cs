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
        ChatController ChatCon;
        private readonly DBContext dbContext;
        public MessageController(DBContext dBContext)
        {
            this.dbContext = dBContext;
            ChatCon = new ChatController(dBContext);
        }

        public List<Message> GetMessages(int offset, int count, int user, int chat)
        {
            Chat ch = ChatCon.GetChat(user, chat);
            
            return dbContext.Messages.Select(x => ch.messages.Contains(x.ID)).ToList();
        }
        public void AddMessage(string text, List<int> repliedFrom, List<int> attachments, int user, int chat)
        {
            Chat ch = ChatCon.GetChat(user, chat);
            dbContext.Messages.Add(new Message
            { content = text,
              author = user,
              deleted = false, 
              edited = false,
              repliedFrom = repliedFrom,
              attachments = attachments }) ;
            dbContext.SaveChanges();
            List<Message>added = dbContext.Messages.TakeLast(1).ToList();
            ch.messages.Add(added[0].ID);
            dbContext.Chats.Update(ch);
            dbContext.SaveChanges();
        }
        public void EditMessage(int message, string text, List <int> attachments, List<int> repliedFrom, int user, int chat)
        {
            var mes = dbContext.Messages.Find(message);
            mes.content = text;
            mes.attachments = attachments;
            mes.repliedFrom = repliedFrom;
            mes.edited = true;
            dbContext.Messages.Update(mes);
            dbContext.SaveChanges();
        }
        public void DeleteMessages(List<int> messages, int user, int chat)
        {
            for (int i = 0; i < messages.Count; i++)
            {
                var mes = dbContext.Messages.Find(messages[i]);
                mes.deleted = true;
                dbContext.Messages.Update(mes);
            }
            dbContext.SaveChanges();
        }
    }
}
