using System.Collections.Generic;
using СhatService.Contract;

namespace ChatService.BusinessLogic
{
    public class SendMessageRequest
    {
        public string text;
        public List<int> repliedFrom;
        public List<Attachment> attachments;
    }
}