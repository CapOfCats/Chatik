using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace СhatService.Contract
{
    public class ChatHub : Hub
    {
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
