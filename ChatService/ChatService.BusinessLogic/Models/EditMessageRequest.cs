using System.Collections.Generic;
using СhatService.Contract;

namespace ChatService.BusinessLogic
{
    public class EditMessageRequest
    {
        public int id;
        public string text;
        public List<Attachment> attachments;
        public List<int> repliedfrom;
    }
}