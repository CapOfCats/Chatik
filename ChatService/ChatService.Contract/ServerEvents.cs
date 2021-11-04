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
        // TODO все методы - public
        // TODO нормальное форматирование

        /// <summary>
        /// Отправляет обновленный чат
        /// </summary>
        static void UpdateChat(IHubCallerClients Clients, Chat chat)
        {

            Clients.Clients(
                Program.connections
                    .Where(c => c.chat == chat.ID)
                    .Select(c => c.connectionID)
            )
                .SendAsync("UpdateChat", chat);
        }

        static void UpdateChat(IHubCallerClients Clients, Chat chat, User user)
        {
            Clients.Clients(
                Program.connections
                    .Where(c => c.user == user.ID)
                    .Select(c => c.connectionID)
            )
                .SendAsync("UpdateChat", chat);
        }
        /// <summary>
        /// Отправляет обновленные сообщения
        /// </summary>
        static void UpdateMessages(IHubCallerClients Clients, Chat chat, object[] messages, Attachment[] attachments)
        {
            // TODO добавить в messages параметр hide(отвеченные сообщения)
            // TODO использовать messagesCount
            Clients.Clients(Program.connections
                .Where(c => c.chat == chat.ID)
                .Select(c => c.connectionID).ToList())
                .SendAsync("UpdateMessages", messages, attachments);
        }

        /// <summary>
        /// Отправляет обновленные данные о пользователе
        /// </summary>
        static void UpdateUsers(IHubCallerClients Clients, Chat chat, object[] users)
        {
            // Убрать из пользователя лишние параметры. Например, пользователь на клиенте не должен знать список чатов других пользователей(см аналитику)
            Clients.Clients(Program.connections
                .Where(c => c.chat == chat.ID)
                .Select(c => c.connectionID).ToList())
                .SendAsync("UpdateUsers", users);
        }
    }
}
