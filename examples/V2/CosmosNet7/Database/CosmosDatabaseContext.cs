using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos;

namespace WebApp.Database
{
    public class CosmosDatabaseContext : DbContext
    {
        public CosmosDatabaseContext(DbContextOptions<CosmosDatabaseContext> options) : base(options)
        {
        }

        public DbSet<ExampleClass> ExampleClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultContainer("ServiceQueryContainer");
            modelBuilder.Entity<ExampleClass>().HasKey(x => x.Id);
        }
    }
}