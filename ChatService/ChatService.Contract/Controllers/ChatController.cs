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
        }
        public Chat GetChat(int user, int chatID)
        {
            var chat = dbContext.Chats
                .Find(chatID);
            if (chat.users.Contains(user))
                return chat;
            else return null;
        }
    }
}
