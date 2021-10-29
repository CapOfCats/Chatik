using System;

namespace СhatService.Contract
{
    public class Message
    {
        // <summary>
        // Идентификатор сообщения
        // </summary>
        public string ID;

        // <summary>
        // Текст сообщения
        // </summary>
        public string content;

        // <summary>
        // Дата отправки сообщения
        // </summary>
        public DateTime date;

        // <summary>
        // Идентификаторы пользователей, прочитавших сообщение
        // </summary>
        public string[] readBy;

        // <summary>
        // Идентификаторы сообщений, на которые был дан ответ
        // </summary>
        public string[] repliedFrom;

        // <summary>
        // Идентификаторы вложений, прикрепленных к сообщению
        // </summary>
        public string[] attachments;

        // <summary>
        // Редактировось ли сообщение
        // </summary>
        public bool edited;

        // <summary>
        // Было ли сообещние удалено
        // </summary>
        public bool deleted;

        // <summary>
        // Идентификатор пользователя-автора сообщения
        // </summary>
        public string author;
    }
}
