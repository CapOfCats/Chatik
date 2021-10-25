using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СhatService.Contract
{ /// <summary>
  /// Вспомогательный класс серверных событий
  /// </summary>
    class ServerEvents
    {
        /// <summary>
        /// Метод,принимающий на вход экземпляр Chat. Предполагается, как ивент, отправляющий пользователю чат Connection.Send
        /// </summary>
        static void UpdateChat(Chat chat)
        {
            
        }
        /// <summary>
        /// Метод,принимающий на вход Массив словарей по message-ключу и hide-значению тех сообщений,что нужно показать, если они родом из текущего чата(вместе с вложениями).
        /// if(!hide) , отправляет пользователю это сообщение
        /// </summary>
        /// <param name="messages">Нужен чтобы передавать сообщения</param>
        static void UpdateMessages(object[] messages, Attachment[] attachments)
        {
                        
        }
        /// <summary>
        /// Метод,принимающий на вход данные о юзерах. Предполагается, как ивент, отправляющий пользователю данные по другим пользователям в чатах. 
        /// </summary>
        ///  <param name="users">Нужен чтобы передавать пользователей</param>
        static void UpdateUsers(object[] users)
        {
            
        }
    }
}
