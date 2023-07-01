using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace ServiceQuery.Xunit
{
    public class SqlServerInMemHelper
    {
        public static IQueryable<TestClass> GetTestList()
        {
            var builder = new DbContextOptionsBuilder<SqlServerInMemTestContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var _context = new SqlServerInMemTestContext(builder.Options);
            var list = new TestClass().GetDefaultList();
            foreach (var item in list)
                _context.TestClasses.Add(item);
            _context.SaveChanges();
            return _context.TestClasses.AsQueryable<TestClass>();
        }
    }
}