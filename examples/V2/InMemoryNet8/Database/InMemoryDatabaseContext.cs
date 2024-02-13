using Microsoft.EntityFrameworkCore;

namespace WebApp.Database
{
    public class InMemoryDatabaseContext : DbContext
    {
        public InMemoryDatabaseContext(DbContextOptions<InMemoryDatabaseContext> options) : base(options)
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