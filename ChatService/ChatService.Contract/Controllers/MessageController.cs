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
        private readonly DBContext dbContext;
        public MessageController(DBContext dBContext)
        {
            this.dbContext = dBContext;
            //dbContext.Messages.Add(new Message { content = "kazino ebani vrot" });
           // dbContext.SaveChanges();
        }
        public Message[] GetMessages(int offset, int count, int user, int chat)
        {
            return dbContext.Messages.OrderByDescending(x => x.date).Skip(offset).Take(count).ToArray();
        }
        public void AddMessage(string text, List<int> repliedF, List<int> att, int user, int chat)
        {
            dbContext.Messages.Add(new Message { content = text, author = user, deleted=false, edited=false, repliedFrom=repliedF, attachments=att }) ;
            dbContext.SaveChanges();
        }
        public void EditMessage(int message, string text, List <int> attachments, List<int> repliedFrom, int user, int chat)
        {
            dbContext.Database.ExecuteSqlRaw($"UPDATE Messages SET content={text},attachments={attachments},repliedFrom={repliedFrom},edited=TRUE WHERE ID={message}");
            dbContext.SaveChanges();
        }
        public void DeleteMessages(List<int> messages, int user, int chat)
        {
            for (int i = 0; i < messages.Count; i++)
            {
                var messes = dbContext.Messages.Where(m => m.ID == messages[i]).ToList();
                dbContext.Messages.Remove(messes[i]);
                dbContext.SaveChanges();
            }            
        }
    }
}
