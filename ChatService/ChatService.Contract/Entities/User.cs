using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace СhatService.Contract
{
    public class User
    {
        public enum Role
        {
            Customer = 0,
            Vendor = 1,
            Support = 2,
            Admin = 10
        }

        /// <summary>
        /// Иденификатор пользователя
        /// </summary>
        [Key]
        public string ID;

        /// <summary>
        /// Ссылка или контент аватара пользователя
        /// </summary>
        public string avatar;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string name;

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string surname;

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public Role[] roles;

        /// <summary>
        /// Идентификаторы чатов, в которые входит пользователь
        /// </summary>
        [ForeignKey("ID")]
        public List<Chat> chats;

        /// <summary>
        /// Время последней активности пользователя
        /// </summary>
        public DateTime lastActivity;
    }
   
}
