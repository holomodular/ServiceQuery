using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos;

namespace ServiceQuery.Xunit
{
    public class CosmosTestContext : DbContext
    {
        public CosmosTestContext(DbContextOptions<CosmosTestContext> options) : base(options)
        {
        }

        public DbSet<TestClass> TestClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Logging");

            modelBuilder.Entity<TestClass>().HasKey(x => x.CosmosKey);
            modelBuilder.Entity<TestClass>().Property(x => x.CosmosKey).ValueGeneratedOnAdd();
            modelBuilder.Entity<TestClass>().Ignore(x => x.NullByteArrayVal);

            base.OnModelCreating(modelBuilder);
        }
    }
}