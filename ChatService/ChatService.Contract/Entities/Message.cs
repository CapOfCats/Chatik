using System;

namespace СhatService.Contract
{
    public class Message
    {
        string ID;
        string content;
        DateTime date;
        string[] readBy;
        string[] repliedFrom;
        string[] attachments;
        bool edited;
        bool deleted;
        string author;

        public Message(
            string ID,
            string content,
            DateTime date,
            string[] readBy,
            string[] repliedFrom,
            string[] attachments,
            bool edited,
            bool deleted,
            string author
        )
        {
            this.ID = ID;
            this.content = content;
            this.date = date;
            this.readBy = readBy;
            this.repliedFrom = repliedFrom;
            this.attachments = attachments;
            this.edited = edited;
            this.deleted = deleted;
            this.author = author;
        }
    }
}
