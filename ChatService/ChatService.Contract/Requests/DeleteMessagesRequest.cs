using System.Collections.Generic;

namespace СhatService.Contract
{
    public class DeleteMessagesRequest
    {
        public List<int> IDs;
        public bool deleteForAll;
    }
}