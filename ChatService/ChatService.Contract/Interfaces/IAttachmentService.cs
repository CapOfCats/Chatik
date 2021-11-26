using System.Collections.Generic;
using System.Threading.Tasks;

namespace СhatService.Contract
{
    public interface IAttachmentService
    {
        /// <summary>
        /// Получение вложений
        /// </summary>
        public Task<List<Attachment>> GetAttachments(List<int> attachmentsIDs);

        /// <summary>
        /// Добавление вложений
        /// </summary>
        public Task<List<int>> AddAttachments(List<Attachment> attachments);
    }
}
