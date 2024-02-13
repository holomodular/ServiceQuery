using Microsoft.EntityFrameworkCore;

namespace WebApp.Database
{
    public class SqliteDatabaseContext : DbContext
    {
        public SqliteDatabaseContext(DbContextOptions<SqliteDatabaseContext> options) : base(options)
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