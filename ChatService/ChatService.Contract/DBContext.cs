using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using СhatService.Contract;

namespace ChatService.Contract
{
    public class DBContext : DbContext
    {
        public DbSet<Attachment> Attachments;
        public DbSet<Chat> Chats;
        public DbSet<Message> Messages;
        public DbSet<User> Users;

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}