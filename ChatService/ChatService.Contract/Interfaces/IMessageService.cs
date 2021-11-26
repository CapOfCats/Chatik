using System.Collections.Generic;
using System.Threading.Tasks;

namespace СhatService.Contract
{
    public interface IMessageService
    {
        /// <summary>
        /// Получение сообщения
        /// </summary>
        public Task<List<Message>> GetMessages(int offset, int count, int user, int chat, UserConnection userConnection);
        /// <summary>
        /// Добавление сообщения
        /// </summary>
        public Task AddMessage(string text, List<int> repliedFrom, List<Attachment> attachments, int user, int chat, UserConnection userConnection);
        /// <summary>
        /// Изменение сообщения
        /// </summary>
        /// <param name="attachments">Объект с полями type, src, name, width, height</param>
        public Task EditMessage(int message, string text, List<Attachment> attachments, List<int> repliedFrom, int user, int chat);
        /// <summary>
        /// Удаление сообщений
        /// </summary>
        public Task DeleteMessages(List<int> messages, int user, int chat, bool deleteAll);
    }
}
