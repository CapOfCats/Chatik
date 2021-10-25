using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace Nabrosok
{
    /// <summary>
    /// Вспомогательный класс обработчика клиентских событий
    /// </summary>
    public class ChatHub : Hub
    {
        
        /// <summary>
        /// Метод,получающий чат по id
        /// </summary>
        public void GetChat(int id)
        {
  
        }
        /// <summary>
        /// Метод,получающий нужные сообщения(на вход поз+кол-во)
        /// </summary>
        public void GetMessages(int offset, int count)
        {
                 
        }
        /// <summary>
        /// Метод,отправляющий сообщение
        /// </summary>
        void SendMessage(string text, int[] repliedFrom, int[] attachments, int author)
        {
            
        }
        /// <summary>
        /// Метод,чиститящий сообщения по массиву Айдишников
        /// </summary>
        void DeleteMessages(int[] IDs)
        {
        }
        /// <summary>
        /// Метод,редачащий сообщения 
        /// </summary>
        void EditMessage(int id, string text, int[] attachments, int[] repliedfrom)
        {

        }
        /// <summary>
        /// Метод,сохраняющий значение typing UserConnection
        /// </summary>
        void UserTyping(bool typing)
        {
           
        }
    }
}
