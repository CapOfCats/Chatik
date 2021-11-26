using System.Collections.Generic;
using System.Threading.Tasks;

namespace СhatService.Contract
{
    public interface IChatService
    {
        /// <summary>
        /// Получение чата
        /// </summary>
        public Task<Chat> GetChat(int user, int chat);
    }
}
