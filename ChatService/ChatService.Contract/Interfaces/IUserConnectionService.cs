using System.Collections.Generic;

namespace СhatService.Contract
{
    public interface IUserConnectionService
    {
        public void Connect(UserConnection connection);
        public void Disconnect(UserConnection connection);
        /// <summary>
        /// Изменяет состояние "печатает" пользователя
        /// </summary>
        public void SetTyping(bool isTyping, string user);
    }
}
