namespace СhatService.Contract
{
    public class Attachment
    {
        public enum AttachmentType
        {
            Image = 1
        }

        string ID;
        AttachmentType type;
        string name;
        string src;
        string thumbnail;
        int width;
        int height;

        public Attachment(string ID, AttachmentType type, string name, string src, string thumbnail, int width, int height)
        {
            this.ID = ID;
            this.type = type;
            this.name = name;
            this.src = src;
            this.thumbnail = thumbnail;
            this.width = width;
            this.height = height;
        }
       
    }
}
