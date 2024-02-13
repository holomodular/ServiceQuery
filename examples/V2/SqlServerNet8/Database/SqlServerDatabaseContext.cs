using Microsoft.EntityFrameworkCore;

namespace WebApp.Database
{
    public class SqlServerDatabaseContext : DbContext
    {
        public SqlServerDatabaseContext(DbContextOptions<SqlServerDatabaseContext> options) : base(options)
        {
        }

        public DbSet<ExampleClass> ExampleClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExampleClass>().HasKey(x => x.Id);
        }
    }
}