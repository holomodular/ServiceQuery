using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ServiceQuery.Xunit
{
    public class PostgreSqlHelper
    {
        public static string GetConnectionString()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config.GetConnectionString("PostgreSql");
        }

        public static IQueryable<TestClass> GetTestList()
        {
            var builder = new DbContextOptionsBuilder<PostgreSqlTestContext>();
            builder.UseNpgsql(GetConnectionString());
            var _context = new PostgreSqlTestContext(builder.Options);
            _context.Database.EnsureCreated();
            if (_context.TestClasses.ToList().Count == 0)
            {
                var list = new TestClass().GetDefaultList();
                foreach (var item in list)
                {
                    item.DatabaseKey = 0;
                    _context.TestClasses.Add(item);
                }
                _context.SaveChanges();
            }
            return _context.TestClasses.AsQueryable<TestClass>();
        }
    }
}