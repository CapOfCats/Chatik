using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChatService.Contract;
using Microsoft.AspNetCore.SignalR;
using System.Linq;


namespace СhatService.Contract
{
    public class ChatHub : Hub
    {
        private readonly DBContext dbContext;
        public ChatHub(DBContext dbContext)
        {
            this.dbContext = dbContext;

            // test
            dbContext.Chats.Add(new Chat { title = "hello db" });
            dbContext.SaveChanges();
            Console.WriteLine(dbContext.Chats.ToList()[0].title);
        }

        public void GetChat(int id)
        {
  
        }
        public void GetMessages(int offset, int count)
        {
                 
        }
        void SendMessage(string text, int[] repliedFrom, int[] attachments)
        {
            
        }
        void DeleteMessages(int[] IDs)
        {

        }
        void EditMessage(int id, string text, int[] attachments, int[] repliedfrom)
        {

        }
        void UserTyping(bool typing)
        {
           
        }
    }
}
