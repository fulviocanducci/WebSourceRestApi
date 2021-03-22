using Microsoft.EntityFrameworkCore;
using WebSourceRestApi.Models;
using WebSourceRestApi.Models.Mappings;

namespace WebSourceRestApi.Services
{
    public class DatabaseService : DbContext
    {
        public DatabaseService(DbContextOptions<DatabaseService> options)
            :base(options)
        {
        }

        public DbSet<Todo> Todo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoMapping());
        }
    }
}
