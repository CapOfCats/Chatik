using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using СhatService.Contract;

namespace ChatService.BusinessLogic
{
    public class DBContext : DbContext
    {
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}