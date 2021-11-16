using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace СhatService.Contract
{
    public class User
    {
        public enum ERole
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
        public int ID { get; set; }

        /// <summary>
        /// Ссылка или контент аватара пользователя
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string surname { get; set; }

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public ERole[] roles { get; set; }

        /// <summary>
        /// Идентификаторы чатов, в которые входит пользователь
        /// </summary>
        public List<int> Chats { get; set; }

        /// <summary>
        /// Время последней активности пользователя
        /// </summary>
        public DateTime lastActivity { get; set; }
    }
   
}
