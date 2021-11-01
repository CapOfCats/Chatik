using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using СhatService.Contract;

namespace СhatService.Interfaces
{
    public interface IChatController
    {
        /// <summary>
        /// Получение чата
        /// </summary>
        public Chat GetChat(int user,int chat);
       
    }

    public interface IMessageController
    {
        /// <summary>
        /// Получение сообщения
        /// </summary>
        public Message[] GetMessages(int offset, int count, int user, int chat);
        /// <summary>
        /// Добавление сообщения
        /// </summary>
        public void AddMessage(string text, List<int> repliedFrom, List<int> attachments, int user, int chat);
        /// <summary>
        /// Изменение сообщения
        /// </summary>
        /// <param name="attachments">Объект с полями type, src, name, width, height</param>
        public void EditMessage(int message, string text, List<int> attachments, List<int> repliedFrom, int user, int chat);
        /// <summary>
        /// Удаление сообщений
        /// </summary>
        public void DeleteMessages(List<int> messages, int user, int chat);
    }

    public interface IUserConnectionController
    {
        public void Connect(UserConnection connection);
        public void Disconnect(UserConnection connection);
        /// <summary>
        /// Изменяет состояние "печатает" пользователя
        /// </summary>
        public void SetTyping(bool isTyping, string user);
    }
}
