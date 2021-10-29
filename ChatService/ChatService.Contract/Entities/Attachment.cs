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
        public string ID;

        // <summary>
        // Тип вложения
        // </summary>
        public AttachmentType type;

        // <summary>
        // Название вложения
        // </summary>
        public string name;

        // <summary>
        // Ссылка или контент вложения
        // </summary>
        public string src;

        // <summary>
        // Ссылка или контент миниатюры вложения
        // </summary>
        public string thumbnail;

        // <summary>
        // Ширирна вложения
        // </summary>
        public int width;

        // <summary>
        // Высота вложения
        // </summary>
        public int height;
    }
}
