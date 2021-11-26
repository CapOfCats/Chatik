using System.Collections.Generic;

namespace ChatService.BusinessLogic
{
    public class DeleteMessagesRequest
    {
        public List<int> IDs;
        public bool deleteForAll;
    }
}