using Microsoft.EntityFrameworkCore;


namespace Checkers.Logic
{
    public class DatabaseConnection : DbContext
    {
        public DatabaseConnection() => this.Database.EnsureCreated();

        public DbSet<CheckerMove> HistoryMoves => Set<CheckerMove>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=checkers.db");
        }

        public void AddCheckerMove(CheckerMove checkerMove)
        {
            this.HistoryMoves.Add(checkerMove);
            this.SaveChanges();
        }
    }
}