using Microsoft.EntityFrameworkCore;

namespace Checkers.Logic
{
    public class DatabaseConnection : DbContext
    {
        public DatabaseConnection() => this.Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=checkers.db");
        }
    }
}