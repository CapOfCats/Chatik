using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using СhatService.Contract;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ChatService.BusinessLogic
{
    public class AttachmentsService : IAttachmentService
    {
        private readonly DBContext dbContext;

        public AttachmentsService(DBContext dBContext)
        {
            this.dbContext = dBContext;
        }

        public async Task<List<Attachment>> GetAttachments(List<int> attachmentsIDs)
        {
            return await dbContext.Attachments
                .Where(a => attachmentsIDs.Contains(a.ID))
                .ToListAsync();
        }

        public async Task<List<int>> AddAttachments(List<Attachment> attachments)
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
            await dbContext.SaveChangesAsync();

            return attachments.Select(x => x.ID).ToList();
        }
       
    }
}