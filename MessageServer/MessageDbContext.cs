using MessageServer.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageServer
{
    public class MessageDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public MessageDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;password=1234;username=postgres;Database=messages;Include Error Detail=true");
        }

    }


}
