using System.Collections.Generic;

namespace СhatService.Contract
{
    public class EditMessageRequest
    {
        public int id;
        public string text;
        public List<Attachment> attachments;
        public List<int> repliedfrom;
    }
}