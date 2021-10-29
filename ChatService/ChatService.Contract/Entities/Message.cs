using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace СhatService.Contract
{
    public class Message
    {
        // <summary>
        // Идентификатор сообщения
        // </summary>
        [Key]
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
        // Пользователи, прочитавшие сообщение
        // </summary>
        [ForeignKey("ID")]
        public List<User> readBy;

        // <summary>
        // Сообщения, на которые был дан ответ
        // </summary>
        [ForeignKey("ID")]
        public List<Message> repliedFrom;

        // <summary>
        // Вложения, прикрепленные к сообщению
        // </summary>
        [ForeignKey("ID")]
        public List<Attachment> attachments;

        // <summary>
        // Редактировось ли сообщение
        // </summary>
        public bool edited;

        // <summary>
        // Было ли сообещние удалено
        // </summary>
        public bool deleted;

        // <summary>
        // Пользователь-автор сообщения
        // </summary>
        [ForeignKey("ID")]
        public User author;
    }
}
