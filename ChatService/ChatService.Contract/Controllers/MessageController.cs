using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СhatService.Interfaces;

namespace СhatService.Contract
{
    class MessageController : IMessageController
    {
        public Message[] GetMessages(int offset, int count, string user, string chat)
        {
            return new Message[] { new Message() };
        }
        public void AddMessage(string text, string[] repliedFrom, Attachment[] attachments, string user, string chat)
        {
           
        }
        public void EditMessage(string message, string text, object[] attachments, string[] repliedFrom, string user, string chat)
        {
           
        }
        public void DeleteMessages(string[] messages, string user, string chat)
        {

        }
    }
}
