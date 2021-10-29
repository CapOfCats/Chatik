using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace СhatService.Contract
{
    public class Chat
    {
        // <summary>
        // Идентификатор чата
        // </summary>
        [Key]
        public string ID;

        // <summary>
        // Заголовок чата
        // </summary>
        public string title;

        // <summary>
        // Пользователи, входящие в чат
        // </summary>
        [ForeignKey("ID")]
        public List<User> users;

        // <summary>
        // Сообщения, входящие в чат
        // </summary>
        [ForeignKey("ID")]
        public List<Message> messages;
    }
}
