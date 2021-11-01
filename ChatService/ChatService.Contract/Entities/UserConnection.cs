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
        public int chat;

        /// <summary>
        /// Печатает ли пользователь в подключении
        /// </summary>
        public bool typing;

        /// <summary>
        /// Количество сообщений, доступных пользователю для просмотра на данный момент
        /// </summary>
        public int messagesCount;
        
        /// <summary>
        /// Индификатор соединения
        /// </summary>
        public string connectionID;
    }
}
