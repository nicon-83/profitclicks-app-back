using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyWebApi.Models
{
    public class MyWebApiContext : DbContext
    {
        private readonly string connectionString;

        public MyWebApiContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Phone> Phones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}