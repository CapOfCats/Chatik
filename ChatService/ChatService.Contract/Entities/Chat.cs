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
        public int ID { get; set; }

        // <summary>
        // Заголовок чата
        // </summary>
        public string title { get; set; }

        // <summary>
        // Пользователи, входящие в чат
        // </summary>
        public List<int> users { get; set; }

        // <summary>
        // Сообщения, входящие в чат
        // </summary>
        public List<int> messages { get; set; }
    }
}
