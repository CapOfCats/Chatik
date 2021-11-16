using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;

namespace СhatService.Contract
{ /// <summary>
  /// Вспомогательный класс серверных событий
  /// </summary>
    class ServerEvents
    {
        public IHubCallerClients Clients;

        public ServerEvents(IHubCallerClients Clients)
        {
            this.Clients = Clients;
        }

        /// <summary>
        /// Отправляет обновленный чат
        /// </summary>
        public void UpdateChat(string connectionID, Chat chat)
        {
            chat.messages = null;

            Clients.Clients(connectionID)
                .SendAsync("UpdateChat", chat);
        }

        public void UpdateChat(List<string> connectionIDs, Chat chat)
        {
            chat.messages = null;

            Clients.Clients(connectionIDs)
                .SendAsync("UpdateChat", chat);
        }

        /// <summary>
        /// Отправляет обновленные сообщения
        /// </summary>
        public void UpdateMessages(int chatID, List<Message> messages, List<Message> hiddenMessages, List<Attachment> attachments)
        {
            List<UserConnection> userConnections = Program.connections.Where(c => c.chat == chatID).ToList();

            userConnections.ForEach(connection => {
                Clients.Clients(connection.connectionID)
                    .SendAsync(
                        "UpdateMessages",
                        messages
                            .Select(m => new {
                                ID = m.ID,
                                content = m.content,
                                date = m.date,
                                readBy = m.readBy,
                                repliedFrom = m.repliedFrom,
                                attachments = m.attachments,
                                edited = m.edited,
                                author = m.author,
                                deleted = m.deletedForAll || m.usersDelete.Contains(connection.user),
                                hide = false,
                            })
                            .Union(
                                messages
                                    .Select(m => new {
                                        ID = m.ID,
                                        content = m.content,
                                        date = m.date,
                                        readBy = m.readBy,
                                        repliedFrom = m.repliedFrom,
                                        attachments = m.attachments,
                                        edited = m.edited,
                                        author = m.author,
                                        deleted = m.deletedForAll || m.usersDelete.Contains(connection.user),
                                        hide = true,
                                    })
                            ),
                        attachments
                );
            });
        }

        /// <summary>
        /// Отправляет обновленные данные о пользователе
        /// </summary>
        public void UpdateUsers(int chat, List<User> users)
        {
            Clients.Clients(
                Program.connections
                    .Where(c => c.chat == chat)
                    .Select(c => c.connectionID).ToList()
            )
                .SendAsync("UpdateUsers", users.Select(u => new { 
                    ID = u.ID,
                    avatar = u.avatar,
                    name = u.name,
                    surname = u.surname,
                    roles = u.roles,
                    // TODO 15 minutes
                    // online = u.lastActivity > 
                    typing = Program.connections.Find(c => c.user == u.ID && c.chat == chat).typing,
                }));
        }
    }
}
