namespace СhatService.Contract
{
    public class Chat
    {
        string ID;
        string title;
        string[] users;
        string[] messages;

        public Chat()
        {
        }

        public Chat(string ID, string title, string[] users, string[] messages)
        {
            this.ID = ID;           
            this.title = title;
            this.users = users;
            this.messages = messages;
        }
    }
}
