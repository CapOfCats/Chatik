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
   class ChatController : IChatController
    {
        private readonly DBContext dbContext;
        public ChatController(DBContext dBContext)
        {
            this.dbContext = dBContext;
            //dbContext.Messages.Add(new Message { content = "ebal rot SQL" });
            // dbContext.SaveChanges();
            //dbContext.Chats.Where(c => c.ID == chat);
        }
        public Chat GetChat(int user,int chat)
        {          
            return dbContext.Chats.Find(chat);
        }
    }
}
//Входные,коннект,конвертация,кр,конструктор,нулл,интерфейсы,порядок,недостающие данные,проблемы хаба
//через юз кон