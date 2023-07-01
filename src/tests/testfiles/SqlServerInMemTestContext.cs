using Microsoft.EntityFrameworkCore;

namespace ServiceQuery.Xunit
{
    public class SqlServerInMemTestContext : DbContext
    {
        public SqlServerInMemTestContext(DbContextOptions<SqlServerInMemTestContext> options) : base(options)
        {
        }

        public DbSet<TestClass> TestClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TestClass>().HasKey(x => x.DatabaseKey);
            modelBuilder.Entity<TestClass>().Ignore(x => x.NullByteArrayVal);
        }
    }
}