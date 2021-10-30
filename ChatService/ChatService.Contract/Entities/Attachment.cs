using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace СhatService.Contract
{
    public class Attachment
    {
        public enum AttachmentType
        {
            Image = 1
        }

        // <summary>
        // Идентификатор вложения
        // </summary>
        [Key]
        public int ID { get; set; }

        // <summary>
        // Тип вложения
        // </summary>
        public AttachmentType type { get; set; }

        // <summary>
        // Название вложения
        // </summary>
        public string name { get; set; }

        // <summary>
        // Ссылка или контент вложения
        // </summary>
        public string src { get; set; }

        // <summary>
        // Ссылка или контент миниатюры вложения
        // </summary>
        public string thumbnail { get; set; }

        // <summary>
        // Ширирна вложения
        // </summary>
        public int width { get; set; }

        // <summary>
        // Высота вложения
        // </summary>
        public int height { get; set; }
    }
}
