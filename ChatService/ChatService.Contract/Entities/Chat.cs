namespace СhatService.Contract
{
    public class Chat
    {
        // <summary>
        // Идентификатор чата
        // </summary>
        public string ID;

        // <summary>
        // Заголовок чата
        // </summary>
        public string title;

        // <summary>
        // Идентификаторы пользоватей, входящих в чат
        // </summary>
        public string[] users;

        // <summary>
        // Идентификатор сообщений, входящих в чат
        // </summary>
        public string[] messages;
    }
}
