namespace СhatService.Contract
{
    public class UserConnection
    {
        /// <summary>
        /// Идентификатор пользователя подключения
        /// </summary>
        public string user;

        /// <summary>
        /// Идентификатор активного чата подключения
        /// </summary>
        public string chat;

        /// <summary>
        /// Печатает ли пользователь в подключении
        /// </summary>
        public string typing;

        /// <summary>
        /// Количество сообщений, доступных пользователю для просмотра на данный момент
        /// </summary>
        public int messagesCount;
    }
}
