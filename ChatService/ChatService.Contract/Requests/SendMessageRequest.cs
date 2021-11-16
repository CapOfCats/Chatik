using System.Collections.Generic;

namespace СhatService.Contract
{
    public class SendMessageRequest
    {
        public string text;
        public List<int> repliedFrom;
        public List<Attachment> attachments;
    }
}