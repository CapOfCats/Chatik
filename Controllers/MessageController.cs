using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabrosok
{/// <summary>
 /// Контроллер  Message
 /// </summary>
    static class MessageController
    {
        /// <summary>
        /// Метод,сохраняющий сообщения.
        /// </summary>
        static void AddMessage(string text, string[] repliedFrom, Attachment[] attachments, string user, string chat)
        {
           
        }
        /// <summary>
        /// Метод,сохраняющий изменённое сообщение. 
        /// </summary>
        /// <param name="attachments">Нужен чтобы передавать вложения</param>
        static Message EditMessage(string message, string text, object[] attachments, string[] repliedFrom, string user, string chat)
        {
           
        }
        /// <summary>
        /// Метод,удаляющий сообщения с БД.
        /// </summary>
        static void DeleteMessages(string[] messages, string user, string chat)
        {
            //Удаляет мэсседжи в клиенте
            //На вход - UUID массив айдишников сообщений, айдишник автора, и айдишник чата
        }
        /// <summary>
        /// Метод, возвращающий сообщения.
        /// </summary>
        static Message[] GetMessages(int offset, int count, string user, string chat)
        {
            //Запрашивает у клиента массив сообщений
            //На вход позиция, с которой начинается счет и количество сообщений. Айди юзера и чата. 
            //В цикле запихивает экземпляры в результирующий массив
            //ретурним messages[]
        }
    }
}
