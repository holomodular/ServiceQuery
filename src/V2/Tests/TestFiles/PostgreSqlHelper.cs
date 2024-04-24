using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ServiceQuery.Xunit.Integration
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

            var list = new TestClass().GetDefaultList();
            var templist = _context.TestClasses.ToList();

            // Delete previous records
            if (templist.Count > 0)
            {
                foreach (var item in templist)
                    _context.TestClasses.Remove(item);
                _context.SaveChanges();
            }

            // Add new records
            foreach (var item in list)
            {
                item.DatabaseKey = 0;
                _context.TestClasses.Add(item);
            }
            _context.SaveChanges();

            return _context.TestClasses.AsQueryable<TestClass>();
        }

        public static IQueryable<TestClass> GetTestNullCopyList()
        {
            var builder = new DbContextOptionsBuilder<PostgreSqlTestContext>();
            builder.UseNpgsql(GetConnectionString());
            var _context = new PostgreSqlTestContext(builder.Options);
            _context.Database.EnsureCreated();

            var list = new TestClass().GetDefaultList();
            var templist = _context.TestClasses.ToList();

            // Delete previous records
            if (templist.Count > 0)
            {
                foreach (var item in templist)
                    _context.TestClasses.Remove(item);
                _context.SaveChanges();
            }

            // Add new records
            foreach (var item in list)
            {
                item.CopyToNullVals();
                item.DatabaseKey = 0;
                _context.TestClasses.Add(item);
            }
            _context.SaveChanges();

            return _context.TestClasses.AsQueryable<TestClass>();
        }
    }
}