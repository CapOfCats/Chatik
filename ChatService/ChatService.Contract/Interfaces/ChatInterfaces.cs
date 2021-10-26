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
        public Chat GetChat(string user, string chat);
       
    }

    public interface IMessageController
    {
        /// <summary>
        /// Получение сообщения
        /// </summary>
        public Message[] GetMessages(int offset, int count, string user, string chat);
        /// <summary>
        /// Добавление сообщения
        /// </summary>
        public void AddMessage(string text, string[] repliedFrom, Attachment[] attachments, string user, string chat);
        /// <summary>
        /// Изменение сообщения
        /// </summary>
        /// <param name="attachments">Объект с полями type, src, name, width, height</param>
        public void EditMessage(string message, string text, object[] attachments, string[] repliedFrom, string user, string chat);
        /// <summary>
        /// Удаление сообщений
        /// </summary>
        public void DeleteMessages(string[] messages, string user, string chat);
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
