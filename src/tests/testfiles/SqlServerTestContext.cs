using Microsoft.EntityFrameworkCore;

namespace ServiceQuery.Xunit
{
    public class SqlServerTestContext : DbContext
    {
        public SqlServerTestContext(DbContextOptions<SqlServerTestContext> options) : base(options)
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