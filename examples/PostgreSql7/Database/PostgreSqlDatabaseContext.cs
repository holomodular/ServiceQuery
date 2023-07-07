using Microsoft.EntityFrameworkCore;

namespace WebApp.Database
{
    public class PostgreSqlDatabaseContext : DbContext
    {
        public PostgreSqlDatabaseContext(DbContextOptions<PostgreSqlDatabaseContext> options) : base(options)
        {
        }

        public DbSet<ExampleClass> ExampleClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExampleClass>().HasKey(x => x.Id);
            modelBuilder.Entity<ExampleClass>().Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}