using System;
using System.Linq;
using СhatService.Contract;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChatService.BusinessLogic
{
    public class ChatsService : IChatService
    {
        private readonly DBContext dbContext;

        public ChatsService(DBContext dBContext)
        {
            this.dbContext = dBContext;
        }

        public async Task<Chat> GetChat(int user, int chatID)
        {
            var chat = await dbContext.Chats
                .Where(c => c.users.Contains(user) && c.ID == chatID)                     
                .FirstOrDefaultAsync();

            if (chat == null)
                throw new Exception("Пользователь не имеет доступа к этому чату/ Чат не найден.");
            else return chat;
        }
    }
}
