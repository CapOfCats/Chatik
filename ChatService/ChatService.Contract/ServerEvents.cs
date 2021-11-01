using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace СhatService.Contract
{ /// <summary>
  /// Вспомогательный класс серверных событий
  /// </summary>
    class ServerEvents
    {
        /// <summary>
        /// Отправляет обновленный чат
        /// </summary>
        static void UpdateChat(IHubCallerClients Clients, Chat chat)
        {

            Clients.Clients(Program.connections.Where(c => c.chat == chat.ID)
                .Select(c => c.connectionID).ToList())
                .SendAsync("UpdateChat", chat);

        }
        /// <summary>
        /// Отправляет обновленные сообщения
        /// </summary>
        static void UpdateMessages(IHubCallerClients Clients, Chat chat, object[] messages, Attachment[] attachments)
        {
            Clients.Clients(Program.connections.Where(c => c.chat == chat.ID)
                .Select(c => c.connectionID).ToList())
                .SendAsync("UpdateMessages", messages, attachments);
        }
        /// <summary>
        /// Отправляет обновленные данные о пользователе
        /// </summary>
        static void UpdateUsers(IHubCallerClients Clients, Chat chat, object[] users)
        {
            Clients.Clients(Program.connections.Where(c => c.chat == chat.ID)
                .Select(c => c.connectionID).ToList())
                .SendAsync("UpdateUsers", users);
        }
    }
}
