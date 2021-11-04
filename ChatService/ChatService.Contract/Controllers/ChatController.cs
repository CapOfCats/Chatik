using System;
using СhatService.Interfaces;
using ChatService.Contract;
using System.Linq;

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
                .Where(x => x.users.Contains(user) && x.ID == chatID)                     
                .FirstOrDefault();
            if (chat == null)
                throw new Exception("Пользователь не имеет доступа к этому чату/ Чат не найден.");
            else return chat;
        }
    }
}
