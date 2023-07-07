﻿using Microsoft.EntityFrameworkCore;

namespace ServiceQuery.Xunit
{
    public class PostgreSqlTestContext : DbContext
    {
        public PostgreSqlTestContext(DbContextOptions<PostgreSqlTestContext> options) : base(options)
        {
        }

        public DbSet<TestClass> TestClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TestClass>().HasKey(x => x.DatabaseKey);
            modelBuilder.Entity<TestClass>().Property(x => x.DatabaseKey).ValueGeneratedOnAdd();
            modelBuilder.Entity<TestClass>().Ignore(x => x.NullByteArrayVal);
        }
    }
}