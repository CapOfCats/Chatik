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
       
        public ChatHub(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }
            public void GetChat(int id)
        {

            var ch = ChatController.GetChat(conn.user, id);
        }
        public void GetMessages(int offset, int count)
        {
            Message[] messes = MessageController.GetMessages(offset, count, conn.user, conn.chat);
            //кросс с клиентом
        }
        void SendMessage(string text, List<int> repliedFrom, List<int> attachments)
        {
            MessageController.AddMessage(text, repliedFrom, attachments, conn.user, conn.chat);
            //Апдейт на сервере
        }
        void DeleteMessages(List<int> IDs)
        {
            MessageController.DeleteMessages(IDs, conn.user, conn.chat);
            //Апдейт на сервере
        }
        void EditMessage(int id, string text, List<int> attachments, List<int> repliedfrom)
        {
            MessageController.EditMessage(id, text, attachments, repliedfrom, conn.user, conn.chat);
        }
        void UserTyping(bool typing)
        {
           
        }
    }
}
