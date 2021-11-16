using System.Collections.Generic;
using System;
using System.Linq;
using СhatService.Interfaces;
using ChatService.Contract;

namespace СhatService.Contract
{
    class AttachmentsController : IAttachmentController
    {
        private readonly DBContext dbContext;

        public AttachmentsController(DBContext dBContext)
        {
            this.dbContext = dBContext;
        }

        public List<Attachment> GetAttachments(List<int> attachmentsIDs)
        {
            return dbContext.Attachments
                .Where(a => attachmentsIDs.Contains(a.ID))
                .ToList();
        }
    }
}