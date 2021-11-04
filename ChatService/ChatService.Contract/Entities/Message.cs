using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace СhatService.Contract
{
    public class Message
    {
        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// Дата отправки сообщения
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// Пользователи, прочитавшие сообщение
        /// </summary>
        public List<int> readBy { get; set; }

        /// <summary>
        /// Сообщения, на которые был дан ответ
        /// </summary>
        public List<int> repliedFrom { get; set; }

        /// <summary>
        /// Вложения, прикрепленные к сообщению
        /// </summary>
        public List<Attachment> attachments { get; set; }

        /// <summary>
        /// Редактировось ли сообщение
        /// </summary>
        public bool edited { get; set;}

        /// <summary>
        /// Список айди юзеров, для которых сообщение удалено
        /// </summary>
        public List<int>usersDelete { get; set; }

        /// <summary>
        /// Пользователь-автор сообщения
        /// </summary>
        public int author { get; set; }
        /// <summary>
        /// Параметр, принимающий истину, если сообщение удалено для всех пользователей(в т.ч. и новых)
        /// </summary>
        public bool deletedForAll { get; set; }
    }
}
