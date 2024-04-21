using Microsoft.EntityFrameworkCore;

namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemTestContext : DbContext
    {
        public SqlServerInMemTestContext(DbContextOptions<SqlServerInMemTestContext> options) : base(options)
        {
        }

        public DbSet<TestClass> TestClasses { get; set; }

        public DbSet<TestNullCopyClass> TestNullCopyClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TestClass>().HasKey(x => x.DatabaseKey);
            modelBuilder.Entity<TestClass>().Ignore(x => x.NullByteArrayVal);

            //modelBuilder.Entity<TestNullCopyClass>().HasKey(x => x.DatabaseKey);
            //modelBuilder.Entity<TestNullCopyClass>().Ignore(x => x.NullByteArrayVal);
        }
    }
}