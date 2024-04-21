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
            modelBuilder.HasDefaultContainer("ServiceQueryTest");

            modelBuilder.Entity<TestClass>().HasKey(x => x.CosmosKey);
            modelBuilder.Entity<TestClass>().Property(x => x.CosmosKey).ValueGeneratedOnAdd();
            modelBuilder.Entity<TestClass>().Ignore(x => x.TimeOnlyVal);
            modelBuilder.Entity<TestClass>().Ignore(x => x.NullByteArrayVal);
            modelBuilder.Entity<TestClass>().Ignore(x => x.NullUInt128Val);
            modelBuilder.Entity<TestClass>().Ignore(x => x.UInt128Val);

            base.OnModelCreating(modelBuilder);
        }
    }
}