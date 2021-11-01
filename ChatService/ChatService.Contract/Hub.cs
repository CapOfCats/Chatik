using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ChatService.Contract;



namespace СhatService.Contract
{
   
    public class ChatHub : Hub
    {
        private readonly DBContext dbContext;
        UserConnection conn = new UserConnection { user = 22, chat = 1, messagesCount = 0, typing = false } ;
        ChatController ChatCon;
        MessageController MesCon;
        public ChatHub(DBContext dbContext)
        {
            this.dbContext = dbContext;
            ChatCon = new ChatController(dbContext);
            MesCon = new MessageController(dbContext);
        }
            public void GetChat(int id)
        {

            var ch = ChatCon.GetChat(conn.user, id);
        }
        public void GetMessages(int offset, int count)
        {
            Message[] messes = MesCon.GetMessages(offset, count, conn.user, conn.chat);
            //кросс с клиентом
        }
        void SendMessage(string text, List<int> repliedFrom, List<int> attachments)
        {
            MesCon.AddMessage(text, repliedFrom, attachments, conn.user, conn.chat);
            //Апдейт на сервере
        }
        void DeleteMessages(List<int> IDs)
        {
            MesCon.DeleteMessages(IDs, conn.user, conn.chat);
            //Апдейт на сервере
        }
        void EditMessage(int id, string text, List<int> attachments, List<int> repliedfrom)
        {
            MesCon.EditMessage(id, text, attachments, repliedfrom, conn.user, conn.chat);
        }
        void UserTyping(bool typing)
        {
           
        }
    }
}
