namespace СhatService.Contract
{
    public class UserConnection
    {
        string user;
        string typing;
        string chat;
        int messagesCount;

        public UserConnection(string user, string typing, string chat, int messagesCount)
        {
            this.user = user;
            this.typing = typing;
            this.chat = chat;
            this.messagesCount = messagesCount;
        }
    }
}
