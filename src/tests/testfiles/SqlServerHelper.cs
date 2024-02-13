using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ServiceQuery.Xunit
{
    public class SqlServerHelper
    {
        public static string GetConnectionString()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config.GetConnectionString("SqlServer");
        }

        public static IQueryable<TestClass> GetTestList()
        {
            var builder = new DbContextOptionsBuilder<SqlServerTestContext>();
            builder.UseSqlServer(GetConnectionString());
            var _context = new SqlServerTestContext(builder.Options);
            _context.Database.EnsureCreated();
            if (_context.TestClasses.Count() != 4)
            {
                var list = new TestClass().GetDefaultList();
                foreach (var item in list)
                    _context.TestClasses.Add(item);
                _context.SaveChanges();
            }
            return _context.TestClasses.AsQueryable<TestClass>();
        }

        public static DbSet<TestClass> GetDbSet()
        {
            var builder = new DbContextOptionsBuilder<SqlServerTestContext>();
            builder.UseSqlServer(GetConnectionString());
            var _context = new SqlServerTestContext(builder.Options);
            _context.Database.EnsureCreated();
            if (_context.TestClasses.Count() != 4)
            {
                var list = new TestClass().GetDefaultList();
                foreach (var item in list)
                    _context.TestClasses.Add(item);
                _context.SaveChanges();
            }
            return _context.TestClasses;
        }
    }
}