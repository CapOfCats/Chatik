using System.Collections.Generic;
using System;
using System.Linq;
using СhatService.Interfaces;
using ChatService.Contract;
using System.IO;

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
        public List<int> AddAttachments(List<Attachment> attachments)
        {
            attachments.ForEach(x =>
            {
                File.WriteAllBytes(
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "Images",
                        x.name
                    ),
                    Convert.FromBase64String(x.src) 
                );
                x.src = Path.Combine("Images", x.name);
            }
            );

            dbContext.Attachments.AddRange(attachments);
            dbContext.SaveChanges();

            return attachments.Select(x => x.ID).ToList();
        }
       
    }
}